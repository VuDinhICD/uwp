using System.Windows.Input;

namespace universal_windows_platform_cs.UserControl
{
    public partial class IconControl : Windows.UI.Xaml.Controls.UserControl
    {
        private string ImageSource { get; set; }
        private ICommand IconICommand { get; set; }
        private string IconCommandParameter { get; set; }
        private readonly string NoImage = "/Assets/icons8-multiply-48.png";

        public IconControl()
        {
            InitializeComponent();
        }

        public string Icon
        {
            get
            {
                if (ImageSource == null)
                {
                    return NoImage;
                }
                return ImageSource;
            }
            set
            {
                switch (value)
                {
                    case "Setting":
                        ImageSource = "/Assets/StoreLogo.png";
                        break;
                    default:
                        ImageSource = NoImage;
                        break;
                }
            }
        }

        public ICommand Command {
            get { return IconICommand; }
            set { IconICommand = (ICommand)value; }
        }

        public string CommandParameter
        {
            get { return IconCommandParameter; }
            set { IconCommandParameter = value; }
        }
    }
}
