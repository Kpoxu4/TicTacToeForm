using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using TicTacToeForm.Resources;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace TicTacToeForm
{
    public partial class TicTacToeForm : Form
    {
        private const int MAXPOINT = 9;
        private const int SIZE = 150;
        private Button[,] buttons;
        private bool isXsTurn = true;
        private int _countPoint = 0;

        public TicTacToeForm()
        {
            this.AutoSize = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeGameBoard();
        }

        private void InitializeGameBoard()
        {
            buttons = new Button[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j] = new Button
                    {
                        Location = new Point(i * SIZE, j * SIZE),
                        Size = new Size(SIZE, SIZE),
                    };
                    buttons[i, j].Click += Button_Click;
                    buttons[i, j].BackColor = Color.Green;
                    Controls.Add(buttons[i, j]);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {

            Button button = sender as Button;
            if (button.BackgroundImage is not null) return;
            GetImage(button);

            if (CheckWin("circle"))
            {
                FindOutWhoWonAndRestartTheGame("Нолики выиграл");
            }
            if (CheckWin("cross"))
            {
                FindOutWhoWonAndRestartTheGame("Крестики выиграл");

            }
            if (_countPoint == MAXPOINT && !CheckWin("cross") && !CheckWin("circle"))
            {
                FindOutWhoWonAndRestartTheGame("Ничья");
            }

        }
        private void FindOutWhoWonAndRestartTheGame(string messagWhoWon)
        {
            MessageBox.Show(messagWhoWon);
            var newGame = new TicTacToeForm();
            this.Hide();
            newGame.Show();
            newGame.FormClosed += (s, args) => this.Close();
        }
        private void GetImage(Button button)
        {
            button.BackgroundImageLayout = ImageLayout.Stretch;
            button.BackgroundImage = isXsTurn ? Resources.Resource.cross : Resources.Resource.circle;
            button.Text = isXsTurn ? "cross" : "circle";
            if (button.Text == "circle") button.ForeColor = button.BackColor;
            _countPoint++;
            isXsTurn = !isXsTurn;
        }

        private bool CheckWin(string playerSymbol)
        {
            var checkWin = false;

            var one = buttons[0, 0].Text;
            var two = buttons[0, 1].Text;
            var three = buttons[0, 2].Text;
            var four = buttons[1, 0].Text;
            var five = buttons[1, 1].Text;
            var six = buttons[1, 2].Text;
            var seven = buttons[2, 0].Text;
            var eight = buttons[2, 1].Text;
            var nine = buttons[2, 2].Text;

            if (one == playerSymbol && two == playerSymbol && three == playerSymbol ||
                four == playerSymbol && five == playerSymbol && six == playerSymbol ||
                seven == playerSymbol && eight == playerSymbol && nine == playerSymbol ||
                one == playerSymbol && four == playerSymbol && seven == playerSymbol ||
                two == playerSymbol && five == playerSymbol && eight == playerSymbol ||
                three == playerSymbol && six == playerSymbol && nine == playerSymbol ||
                one == playerSymbol && five == playerSymbol && nine ==  playerSymbol ||
                three == playerSymbol && five == playerSymbol && seven == playerSymbol) checkWin = true;


            return checkWin;
        }
    }
}
