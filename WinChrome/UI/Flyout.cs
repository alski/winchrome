namespace WinChrome.UI
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Media.Animation;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for FlyOutControl.XAML
    /// </summary>
    [ContentProperty("Content")]
    [TemplatePart(Name = "content", Type = typeof(ContentPresenter))]
    [TemplatePart(Name = "sizer", Type = typeof(ScrollViewer))]
    public class Flyout : ContentControl
    {
        /// <summary>Initialises static members of the <see cref="Flyout"/> class.</summary>
        [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:ElementsMustAppearInTheCorrectOrder", Justification = "I want the statics first")]
        static Flyout()
        {
            // Register that we want to get styles for TargetType="Flyout"
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(Flyout),
                new FrameworkPropertyMetadata(typeof(Flyout)));
        }

        /// <summary>The IsLockedOpen <see cref="DependencyProperty"/>.</summary>
        public static readonly DependencyProperty IsLockedOpenProperty =
             DependencyProperty.Register(
                 "IsLockedOpen",
                 typeof(bool),
                 typeof(Flyout),
                 new PropertyMetadata(IsLockedOpenChanged));

        private static void IsLockedOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Flyout)d).SomethingChanged();
        }

        /// <summary>Used to get <see cref="IsLockedOpen"/> from the designer.</summary>
        /// <param name="window">The window.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool GetIsLockedOpen(UIElement window)
        {
            return (bool)window.GetValue(IsLockedOpenProperty);
        }

        /// <summary>used by the designer to set <see cref="IsLockedOpen"/>.</summary>
        /// <param name="window">The window.</param>
        /// <param name="isLockedOpen">The is locked open.</param>
        public static void SetIsLockedOpen(UIElement window, bool isLockedOpen)
        {
            window.SetValue(IsLockedOpenProperty, isLockedOpen);
        }

        /// <summary>The header <see cref="DependencyProperty"/>.</summary>
        public static readonly DependencyProperty HeaderProperty =
             DependencyProperty.Register(
                 "Header",
                 typeof(UIElement),
                 typeof(Flyout));

        /// <summary>Gets the <see cref="UIElement"/> to be shown in the header.</summary>
        /// <param name="window">The window.</param>
        /// <returns>The <see cref="UIElement"/>.</returns>
        public static UIElement GetHeader(UIElement window)
        {
            return (UIElement)window.GetValue(HeaderProperty);
        }

        /// <summary>Sets the <see cref="UIElement"/> to be shown in the header.</summary>
        /// <param name="window">The window.</param>
        /// <param name="header">The header.</param>
        public static void SetHeader(UIElement window, UIElement header)
        {
            window.SetValue(HeaderProperty, header);
        }

        /// <summary>The <see cref="IsExpanded"/><see cref="DependencyProperty"/>.</summary>
        public static readonly DependencyProperty IsExpandedProperty =
             DependencyProperty.Register(
                 "IsExpanded",
                 typeof(bool),
                 typeof(Flyout));

        /// <summary>Gets if the <see cref="Flyout"/> is expanded.</summary>
        /// <param name="window">The window.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool GetIsExpanded(UIElement window)
        {
            return (bool)window.GetValue(IsExpandedProperty);
        }

        /// <summary>Sets if the <see cref="Flyout"/> is expanded.</summary>
        /// <param name="window">The window.</param>
        /// <param name="isEnabled">The is enabled.</param>
        public static void SetIsExpanded(UIElement window, bool isEnabled)
        {
            window.SetValue(IsExpandedProperty, isEnabled);
        }

        private double _desiredHeight;
        private bool _isDesignMode;


        /// <summary>Initialises a new instance of the <see cref="Flyout"/> class.</summary>
        public Flyout()
        {
            this.IsExpanded = true;
            this._isDesignMode = DesignerProperties.GetIsInDesignMode(this);

            if (!this._isDesignMode)
            {
                this.Loaded += (s, e) => this.InvokeSoon(1000, this.Collapse);
            }

            this.MouseEnter += (s, e) => this.SomethingChanged();
            this.MouseLeave += (s, e) => this.SomethingChanged();
            this.MouseDown += (s, e) => this.SomethingChanged();

            this.StylusEnter += (s, e) => this.SomethingChanged();
            this.StylusLeave += (s, e) => this.SomethingChanged();
            this.StylusDown += (s, e) => this.SomethingChanged();

            this.TouchEnter += (s, e) => this.SomethingChanged();
            this.TouchLeave += (s, e) => this.SomethingChanged();
            this.TouchDown += (s, e) => this.SomethingChanged();

            this.IsKeyboardFocusWithinChanged += (s, e) => this.SomethingChanged();
        }

        /// <summary>Gets or sets a value indicating whether this <see cref="Flyout"/> is expanded.</summary>
        public bool IsExpanded
        {
            get { return GetIsExpanded(this); }
            set { SetIsExpanded(this, value); }
        }

        /// <summary>Gets or sets a value indicating whether this <see cref="Flyout"/>is locked open.</summary>
        public bool IsLockedOpen
        {
            get { return GetIsLockedOpen(this); }
            set { SetIsLockedOpen(this, value); }
        }

        /// <summary>Occurs when the control is expanding, i.e. at start of animation.</summary>
        public event EventHandler<EventArgs> Expanded;

        /// <summary>Occurs when the control is collapsing, i.e. at start of animation.</summary>
        public event EventHandler<EventArgs> Collapsed;

        /// <summary>Something changed handler.</summary>
        private void SomethingChanged()
        {
            if (this.ShouldBeExpanded()
                && (this.IsExpanded == false || this.IsLockedOpen))
            {
                this.Expand();
            }

            if (this.ShouldBeExpanded() == false
                && this.IsExpanded)
            {
                if (this.IsLockedOpen)
                {
                    this.LockOpen();
                }
                else
                {
                    this.InvokeSoon(1000, this.Collapse);
                }
            }
        }

        /// <summary>Works out if the control should be expanded.</summary>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool ShouldBeExpanded()
        {
            return this.IsMouseOver | this.IsKeyboardFocusWithin | this._isDesignMode;
        }

        /// <summary>Queues an action to be invoked back on the <see cref="Dispatcher"/> thread soon.</summary>
        /// <param name="delayMs">The delay in ms.</param>
        /// <param name="action">The action.</param>
        private void InvokeSoon(int delayMs, Action action)
        {
            Task.Factory.StartNew(
                () =>
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(delayMs));
                    this.Dispatcher.Invoke(action);
                });
        }

        /// <summary>Collapses the control.</summary>
        private void Collapse()
        {
            if (!this.IsExpanded || this.ShouldBeExpanded() || this.IsLockedOpen)
            {
                return;
            }

            var temp = Collapsed;
            if (temp != null)
            {
                temp(this, new EventArgs());
            }

            this.IsExpanded = false;

            var content = (ContentPresenter)this.Template.FindName("content", this);
            var sizer = (ScrollViewer)this.Template.FindName("sizer", this);

            content.Measure(new Size(this.ActualWidth, double.PositiveInfinity));
            this._desiredHeight = content.DesiredSize.Height;

            VisualStateManager.GoToState(this, "Collapsed", true);
            sizer.BeginAnimation(
                FrameworkElement.HeightProperty,
                new DoubleAnimation(this._desiredHeight, 0, new Duration(TimeSpan.FromMilliseconds(250))));
        }

        private void LockOpen()
        {
            if (!this.IsExpanded || !this.IsLockedOpen)
            {
                return;
            }

            var temp = Collapsed;
            if (temp != null)
            {
                temp(this, new EventArgs());
            }

            VisualStateManager.GoToState(this, "LockedOpen", true);
        }

        /// <summary>Expands the control.</summary>
        private void Expand()
        {
            this.IsExpanded = true;

            var content = (ContentPresenter)this.Template.FindName("content", this);
            var sizer = (ScrollViewer)this.Template.FindName("sizer", this);

            var temp = Expanded;
            if (temp != null)
            {
                temp(this, new EventArgs());
            }

            VisualStateManager.GoToState(this, "Expanded", true);
            sizer.BeginAnimation(
                FrameworkElement.HeightProperty,
                new DoubleAnimation(sizer.ActualHeight, this._desiredHeight, new Duration(TimeSpan.FromMilliseconds(250))));
        }
    }
}