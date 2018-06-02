using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;
using WordPressPCL.Models;
using WordPressXF.Common;
using WordPressXF.ExtensionMethods;
using WordPressXF.Interfaces;
using WordPressXF.Resources;
using WordPressXF.Services;
using WordPressXF.Utils;
using WordPressXF.Views;
using Xamarin.Forms;

namespace WordPressXF.ViewModels
{
    public class NewsViewModel : BaseViewModel, ISupportIncrementalLoading
    {
        private readonly WordpressService _wordpressService;

        private int _currentPage = -1;

        private ObservableCollection<Post> _posts = new ObservableCollection<Post>();
        public ObservableCollection<Post> Posts
        {
            get => _posts;
            set { _posts = value; OnPropertyChanged(); }
        }

        private Post _selectedPost;
        public Post SelectedPost
        {
            get => _selectedPost;
            set { _selectedPost = value; OnPropertyChanged(); }
        }

        private IEnumerable<CommentThreaded> _comments;
        public IEnumerable<CommentThreaded> Comments
        {
            get => _comments;
            set { _comments = value; OnPropertyChanged(); }
        }

        private string _commentText;
        public string CommentText
        {
            get => _commentText;
            set { _commentText = value; OnPropertyChanged(); PostCommentAsyncCommand.RaiseCanExecuteChange(); }
        }

        private bool _isCommenting = false;
        public bool IsCommenting
        {
            get => _isCommenting;
            set { _isCommenting = value; OnPropertyChanged(); PostCommentAsyncCommand.RaiseCanExecuteChange(); }
        }

        private bool _arePostsNotAvailable = true;
        public bool ArePostsNotAvailable
        {
            get => _arePostsNotAvailable;
            set { _arePostsNotAvailable = value; OnPropertyChanged(); }
        }

        private AsyncRelayCommand _loadPostsAsyncCommand;
        public AsyncRelayCommand LoadPostsAsyncCommand => _loadPostsAsyncCommand ?? (_loadPostsAsyncCommand = new AsyncRelayCommand(LoadPostsAsync));

        private AsyncRelayCommand _postCommentAsyncCommand;
        public AsyncRelayCommand PostCommentAsyncCommand => _postCommentAsyncCommand ?? (_postCommentAsyncCommand = new AsyncRelayCommand(PostCommentAsync, CanPostComment));

        private bool CanPostComment()
        {
            return (!string.IsNullOrEmpty(CommentText) && !IsCommenting);
        }

        private async Task LoadPostsAsync()
        {
            try
            {
                IsLoading = true;

                _currentPage = 0;

                Posts.Clear();

                var posts = (await _wordpressService.GetLatestPostsAsync(_currentPage, PageSize)).ToObservableCollection();
                HasMoreItems = posts.Count == PageSize;

                Posts.AddRange(posts);

                ArePostsNotAvailable = !Posts.Any();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"NewsViewModel | LoadPostsAsync | {ex}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task PostCommentAsync()
        {
            try
            {
                IsCommenting = true;

                if (await _wordpressService.IsUserAuthenticatedAsync())
                {
                    var comment = await _wordpressService.PostCommentAsync(SelectedPost.Id, CommentText);
                    if (comment != null)
                    {
                        CommentText = null;
                        await GetCommentsAsync(SelectedPost.Id);
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(AppResources.CommentDialogNotAuthorizedTitle, AppResources.CommentDialogNotAuthorizedMessage, AppResources.DialogOk);
                }
            }
            finally
            {
                IsCommenting = false;
            }
        }

        private ICommand _selectPostCommand;
        public ICommand SelectPostCommand => _selectPostCommand ?? (_selectPostCommand = new Command<Post>(SelectPost));

        public NewsViewModel(WordpressService wordpressService)
        {
            _wordpressService = wordpressService;
        }

        private async void SelectPost(Post post)
        {
            if (post == null)
                return;

            Comments = null;
            CommentText = null;
            SelectedPost = post;

            await GetCommentsAsync(post.Id);
            await ((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.PushAsync(GetTabbedPage());
        }

        private async Task GetCommentsAsync(int postId)
        {
            IsLoading = true;

            Comments = await _wordpressService.GetCommentsForPostAsync(postId);

            IsLoading = false;
        }

        private TabbedPage GetTabbedPage()
        {
            return new TabbedPage
            {
                Title = HtmlTools.Strip(WebUtility.HtmlDecode(SelectedPost.Title.Rendered)),
                BindingContext = this,
                Children = { new NewsDetailPage(), new CommentPage() }
            };
        }

        #region ISupportIncrementalLoading members

        public int PageSize { get; set; } = 10;

        private bool _isIncrementalLoading;
        public bool IsIncrementalLoading
        {
            get => _isIncrementalLoading;
            set { _isIncrementalLoading = value; OnPropertyChanged(); }
        }

        private bool _hasMoreItems = true;
        public bool HasMoreItems
        {
            get => _hasMoreItems;
            set { _hasMoreItems = value; OnPropertyChanged(); }
        }

        private bool _isLoadingIncrementally;
        public bool IsLoadingIncrementally
        {
            get => _isLoadingIncrementally;
            set { _isLoadingIncrementally = value; OnPropertyChanged(); }
        }

        private ICommand _loadMoreItemsCommand;
        public ICommand LoadMoreItemsCommand => _loadMoreItemsCommand ?? (_loadMoreItemsCommand = new Command(async () => await LoadMoreItemsAsync()));

        private async Task LoadMoreItemsAsync()
        {
            try
            {
                IsLoadingIncrementally = true;

                _currentPage++;

                var posts = (await _wordpressService.GetLatestPostsAsync(_currentPage, PageSize)).ToObservableCollection();
                HasMoreItems = posts.Count == PageSize;

                Posts.AddRange(posts);
                ArePostsNotAvailable = !Posts.Any();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"NewsViewModel | LoadMoreItemsAsync | {ex}");
            }
            finally
            {
                IsLoadingIncrementally = false;
            }
        }

        #endregion
    }
}
