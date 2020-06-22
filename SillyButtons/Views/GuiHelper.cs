using SillyButtons.Properties;
using System.Drawing;
using System.IO;
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
            string resourceFile = $"hangman_remaining_{remainingGuesses}";
            var imageResourceProperty = typeof(Resources).GetProperties(BindingFlags.Static | BindingFlags.Public)
                    .Where(x => x.Name.Equals(resourceFile)).FirstOrDefault();
            if (imageResourceProperty != null)
            {
                return (Bitmap)imageResourceProperty.GetValue(null);
            }
            throw new FileNotFoundException($"Resource file {resourceFile} not found.");
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
