using System;
using System.Collections;
using WordPressXF.Interfaces;
using Xamarin.Forms;

namespace WordPressXF.Controls
{
    public class IncrementalListView : ListView
    {
        public static readonly BindableProperty PreloadCountProperty =
            BindableProperty.Create(nameof(PreloadCount), typeof(int), typeof(IncrementalListView), 5);

        public int PreloadCount
        {
            get => (int)GetValue(PreloadCountProperty);
            set => SetValue(PreloadCountProperty, value);
        }

        private int _lastPosition;
        private IList _itemsSource;
        private ISupportIncrementalLoading _incrementalLoading;

        public IncrementalListView()
        {
            ItemAppearing += OnItemAppearing;
            ItemTapped += (s, e) =>
            {
                if (e.Item == null)
                    return;
                ((IncrementalListView)s).SelectedItem = null;
            };
        }

        ~IncrementalListView()
        {
            ItemAppearing -= OnItemAppearing;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName != ItemsSourceProperty.PropertyName)
                return;

            _itemsSource = ItemsSource as IList;
            if (ItemsSource == null)
                throw new NotSupportedException($"{nameof(IncrementalListView)} requires that {nameof(ItemsSource)} be of type IList");
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext == null)
                return;

            _incrementalLoading = BindingContext as ISupportIncrementalLoading;

            if (_incrementalLoading == null)
                throw new NotSupportedException($"{nameof(IncrementalListView)} BindingContext does not implement {nameof(ISupportIncrementalLoading)}. This is required for incremental loading to work.");
        }

        private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var position = _itemsSource?.IndexOf(e.Item) ?? 0;

            if (_itemsSource == null)
                return;

            if (PreloadCount <= 0)
                PreloadCount = 1;

            var preloadIndex = Math.Max(_itemsSource.Count - PreloadCount, 0);

            if ((position > _lastPosition || position == _itemsSource.Count - 1) && position >= preloadIndex)
            {
                _lastPosition = position;

                if (!_incrementalLoading.IsIncrementalLoading && !IsRefreshing && _incrementalLoading.HasMoreItems)
                    LoadMoreItems();
            }
        }

        private void LoadMoreItems()
        {
            var command = _incrementalLoading.LoadMoreItemsCommand;
            if (command != null && command.CanExecute(null))
                command.Execute(null);
        }
    }
}
