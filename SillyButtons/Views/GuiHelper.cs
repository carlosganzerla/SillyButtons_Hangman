using SillyButtons.Properties;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SillyButtons.Views
{
    public static class GuiHelper
    {
        public static Button GetCharacterButton(this Control control, char letter)
        {
            return control.Controls.OfType<Button>()
                            .FirstOrDefault(x => x.Tag.Equals(letter));
        }
        public static Button CreateAlphabetButton(char letter)
        {
            return new Button
            {
                Text = letter.ToString(),
                Tag = letter,
                Enabled = false
            };
        }

        public static Bitmap GetHangmanImage(int remainingGuesses)
        {
            var property = typeof(Resources).GetProperties(BindingFlags.Static | BindingFlags.Public)
                    .Where(x => x.Name.Equals($"hangman_remaining_{remainingGuesses}")).FirstOrDefault();
            if (property != null)
            {
                return (Bitmap)property.GetValue(null);
            }
            else
            {
                return null;
            }
        }

        public static Label CreateCharScoreLabel(char letter)
        {
            return new Label
            {
                MaximumSize = new Size(25, 50),
                MinimumSize = new Size(25, 50),
                Text = letter.ToString().Replace(" ", "_")
            };
        }
    }
}
