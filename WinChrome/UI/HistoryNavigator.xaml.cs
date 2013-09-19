namespace WinChrome.UI
{
    using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

    /// <summary>
    /// Interaction logic for HistoryNavigator.xaml
    /// </summary>
    [TemplatePart(Name = "PART_ForwardButton", Type=typeof(Button))]
    [TemplatePart(Name = "PART_BackButton", Type=typeof(Button))]
    [TemplatePart(Name="PART_DropButton", Type=typeof(ToggleButton))]
    [TemplatePart(Name="PART_ContextMenu", Type=typeof(ContextMenu))]
    public partial class HistoryNavigator : UserControl
    {
        public static readonly DependencyProperty CommandTargetProperty = DependencyProperty.Register(
            "CommandTarget", typeof(IInputElement), typeof(HistoryNavigator),
            new PropertyMetadata(
                null, new PropertyChangedCallback(OnCommandTargetChanged),
                null),
            null);

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
          "CommandParameter", typeof(object), typeof(HistoryNavigator),
          new PropertyMetadata(
              null, new PropertyChangedCallback(OnCommandParameterChanged),
              null),
          null);

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
         "ItemsSource", typeof(System.Collections.IEnumerable), typeof(HistoryNavigator)
         , //Comment me out for no Notification
         new PropertyMetadata(
              null, new PropertyChangedCallback(OnItemsSourceChanged),
              null),
          null// end comment
         );

         
        public HistoryNavigator()
        {                        
            this.InitializeComponent();
        }
        
        public ICommand BackCommand
        {
            get { return this.PART_BackButton.Command; }
            set { this.PART_BackButton.Command = value; }
        }

        public ICommand ForwardCommand
        {
            get { return this.PART_ForwardButton.Command; }
            set { this.PART_ForwardButton.Command = value; }
        }

        public IInputElement CommandTarget
        {
            get { return (IInputElement)this.GetValue(CommandTargetProperty); }
            set { this.SetValue(CommandTargetProperty, value); }
        }

        public static void OnCommandTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var that = (HistoryNavigator)d;
            that.PART_BackButton.CommandTarget = (IInputElement)e.NewValue;
            that.PART_ForwardButton.CommandTarget = (IInputElement)e.NewValue;       
        }


        public object CommandParameter
        {
            get { return (IInputElement)this.GetValue(CommandParameterProperty); }
            set { this.SetValue(CommandParameterProperty, value); }
        }

        public static void OnCommandParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var that = (HistoryNavigator)d;
            that.PART_BackButton.CommandParameter = e.NewValue;
            that.PART_ForwardButton.CommandParameter = e.NewValue;           
        }

        public System.Collections.IEnumerable ItemsSource
        {
            get { return (System.Collections.IEnumerable)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        public static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var that = (HistoryNavigator)d;
            that.PART_ContextMenu.ItemsSource = (IEnumerable) e.NewValue;
        }

     
        private void PART_DropButton_Click(object sender, RoutedEventArgs e)
        {
            this.PART_ContextMenu.PlacementTarget = this; // PART_DropButton;
            this.PART_ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            this.PART_ContextMenu.IsOpen = true;     
        }

        private void PART_ContextMenu_Closed(object sender, RoutedEventArgs e)
        {
            this.PART_DropButton.IsChecked = false;
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PART_DropButton.Click += this.PART_DropButton_Click;
            this.PART_ContextMenu.Closed += this.PART_ContextMenu_Closed;
        }
    }
}
