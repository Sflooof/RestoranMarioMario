using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RestoranMarioMario.Pages
{
    /// <summary>
    /// Логика взаимодействия для GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        private const int snakeSquareSize = 30;
        private readonly SolidColorBrush snakeColorBrush = Brushes.Green;
        private Rectangle snakeHead;
        private Point foodPosition;
        private static readonly Random randomFoodPosition = new Random();
        private enum Dicection
        {
            Left,
            Right,
            Top,
            Bottom
        }
        private Dicection direction = Dicection.Right;
        private const int timeInterval = 250;
        private DispatcherTimer timer;
        private List<Rectangle> snake = new List<Rectangle>();
        private int score = 0;
        public GamePage()
        {
            InitializeComponent();
        }
        private Rectangle CreateSnake(Point position) //новый квадрат в новых координатах
        {
            Rectangle rectangle = new Rectangle
            {
                Width = snakeSquareSize,
                Height = snakeSquareSize,
                Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("..\\Resources\\head.png"))
                }
            };
            Canvas.SetLeft(rectangle, position.X * snakeSquareSize);
            Canvas.SetTop(rectangle, position.Y * snakeSquareSize);
            return rectangle;
        }

        private void PlaceFood()
        {
            int maxX = (int)(CanvasGame.ActualWidth / snakeSquareSize);
            int maxY = (int)(CanvasGame.ActualHeight / snakeSquareSize);
            int foodX = randomFoodPosition.Next(0, maxX);
            int foodY = randomFoodPosition.Next(0, maxY);
            foodPosition = new Point(foodX, foodY);
            Image foodImage = new Image
            {
                Width = snakeSquareSize,
                Height = snakeSquareSize,
                Source = new BitmapImage(new Uri("..\\Resources\\orange.png"))
            };
            Canvas.SetLeft(foodImage, foodX * snakeSquareSize);
            Canvas.SetTop(foodImage, foodY * snakeSquareSize);
            CanvasGame.Children.Add(foodImage);
        }

        private void GameLoaded(object sender, RoutedEventArgs e)
        {
            InitialGame();
        }
        private void InitialGame()
        {
            snakeHead = CreateSnake(new Point(5, 5));
            snake.Add(snakeHead); //новая часть змеи помещается в список
            CanvasGame.Children.Add(snakeHead);
            PlaceFood();
            timer = new DispatcherTimer();
            timer.Tick += TimerTick;
            timer.Interval = TimeSpan.FromMilliseconds(timeInterval);
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            Point newPosition = CalcuteNewPosition(); //новая позиция
            if (newPosition == foodPosition)
            {
                EatFood();
                PlaceFood();
            }
            if (newPosition.X < 0 || newPosition.Y < 0 ||
                newPosition.X >= CanvasGame.ActualWidth / snakeSquareSize || newPosition.Y >= CanvasGame.ActualHeight / snakeSquareSize)
            {
                timer.Stop();
                BtRestart.Visibility = Visibility.Visible;
                MessageBox.Show("Конец игры!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (snake.Count >= 4)
            {
                for (int i = 0; i < snake.Count; i++)
                {
                    Point currentHeadPosition = new Point(Canvas.GetLeft(snake[i]), Canvas.GetTop(snake[i]));//координата элемента с которым работаю
                    for (int j = i + 1; j < snake.Count; j++) //вроверка элемента currentHeadPosition с позицией другого элемента
                    {
                        Point nextHeadPosition = new Point(Canvas.GetLeft(snake[j]), Canvas.GetTop(snake[j]));
                        if (currentHeadPosition == nextHeadPosition)
                        {
                            timer.Stop();
                            BtRestart.Visibility = Visibility.Visible;
                            MessageBox.Show("Конец игры!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                    }
                }
            }
            for (int i = snake.Count - 1; i > 0; i--) //перемещение позиции элемента змеи на позицию предыдущего элемента
            {
                Canvas.SetLeft(snake[i], Canvas.GetLeft(snake[i - 1]));
                Canvas.SetTop(snake[i], Canvas.GetTop(snake[i - 1]));
            }
            Canvas.SetLeft(snakeHead, newPosition.X * snakeSquareSize);
            Canvas.SetTop(snakeHead, newPosition.Y * snakeSquareSize);
        }
        private Point CalcuteNewPosition()
        {
            double left = Canvas.GetLeft(snakeHead) / snakeSquareSize;
            double top = Canvas.GetTop(snakeHead) / snakeSquareSize;
            Point headCurrentPosotion = new Point(left, top);
            Point newHeadPosition = new Point();
            switch (direction) //позиция головы
            {
                case Dicection.Left:
                    newHeadPosition = new Point(headCurrentPosotion.X - 1, headCurrentPosotion.Y);
                    break;
                case Dicection.Right:
                    newHeadPosition = new Point(headCurrentPosotion.X + 1, headCurrentPosotion.Y);
                    break;
                case Dicection.Top:
                    newHeadPosition = new Point(headCurrentPosotion.X, headCurrentPosotion.Y - 1);
                    break;
                case Dicection.Bottom:
                    newHeadPosition = new Point(headCurrentPosotion.X, headCurrentPosotion.Y + 1);
                    break;
            }
            return newHeadPosition;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    if (direction != Dicection.Bottom)
                        direction = Dicection.Top;
                    break;
                case Key.Down:
                    if (direction != Dicection.Top)
                        direction = Dicection.Bottom;
                    break;
                case Key.Left:
                    if (direction != Dicection.Right)
                        direction = Dicection.Left;
                    break;
                case Key.Right:
                    if (direction != Dicection.Left)
                        direction = Dicection.Right;
                    break;
            }
        }
        private void EatFood()
        {
            score++;
            TbScore.Text = "Счет: " + score.ToString();
            CanvasGame.Children.Remove(CanvasGame.Children.OfType<Image>().FirstOrDefault());
            Rectangle newSnake = CreateSnakeBody(foodPosition); //змея появляется в том мемсте где была еда
            snake.Add(newSnake);
            CanvasGame.Children.Add(newSnake);
        }
        private Rectangle CreateSnakeBody(Point position)
        {
            Rectangle rectangle = new Rectangle
            {
                Width = snakeSquareSize,
                Height = snakeSquareSize,
                Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("..\\Resources\\body.png"))
                }
            };

            Canvas.SetLeft(rectangle, position.X * snakeSquareSize);
            Canvas.SetTop(rectangle, position.Y * snakeSquareSize);
            return rectangle;
        }
        private void BtRestart_Click(object sender, RoutedEventArgs e)
        {
            score = 0;
            TbScore.Text = "Счет: 0";
            CanvasGame.Children.Clear();
            snake.Clear();
            BtRestart.Visibility = Visibility.Collapsed;
            InitialGame();
        }

        private void BtStart_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Змейка – это игра, в которой игрок управляет змеей, которая ползет по экрану и потребляет пищу. " +
                "\nЗмея двигается в четырех направлениях (вверх, вниз, влево, вправо) по сетке клеток с помощью двойного нажатия. " +
                "Когда змея съедает еду, она растет, добавляя новую клетку к своему хвосту. " +
                "\nЗмея не может пересекать свое собственное тело. Если змея ударяется о границу экрана или о собственное тело, игра заканчивается, счет обнуляется и появляется кнопка «Начать заново!». " +
                "Еда появляется случайным образом на экране и представляет собой фрукт.", "Правила игры!", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                InitialGame();
                BtStart.Visibility = Visibility.Collapsed;
            }
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
