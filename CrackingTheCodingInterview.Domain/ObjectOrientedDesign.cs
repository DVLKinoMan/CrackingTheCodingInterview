using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CrackingTheCodingInterview.Domain
{
    public static class ObjectOrientedDesign
    {
        // 7.1 Deck of Cards: Design the data structures for a generic deck of cards. Explain how you would
        // subclass the data structures to implement blackjack.

        #region BlackJack

        public enum Suit
        {
            Club,
            Diamond,
            Heart,
            Spade
        }

        public abstract class Card
        {
            public bool Available = true;
            protected int FaceValue;
            protected Suit Suit { get; }

            public Card(int c, Suit s)
            {
                FaceValue = c;
                Suit = s;
            }

            public abstract int Value();
        }

        public class Deck<T> where T : Card
        {
            private List<T> cards;
            private int dealtIndex = 0;

            public void Shuffle()
            {
            }

            public int RemainingCards => cards.Count - dealtIndex;

            public T[] DealHand(int number)
            {
                throw new NotImplementedException();
            }

            public T DealCard()
            {
                throw new NotImplementedException();
            }
        }

        public class Hand<T> where T : Card
        {
            protected List<T> cards = new List<T>();

            public int Score()
            {
                int score = 0;
                foreach (var card in cards)
                    score += card.Value();

                return score;
            }

            public void AddCard(T card)
            {
                cards.Add(card);
            }
        }

        public class BlackJackCard : Card
        {
            public BlackJackCard(int c, Suit s) : base(c, s)
            {
            }

            public override int Value()
            {
                if (IsAce)
                    return 1;
                if (FaceValue >= 11 && FaceValue <= 13)
                    return 10;
                return FaceValue;
            }

            public int MinValue => IsAce ? 11 : Value();

            public bool IsAce => FaceValue == 1;

            public bool IsFaceCard => FaceValue >= 11 && FaceValue <= 13;
        }

        public class BlackJackHand : Hand<BlackJackCard>
        {
            public new int Score()
            {
                var scores = PossibleScores;
                int maxUnder = int.MinValue, minOver = int.MaxValue;
                foreach (var score in scores)
                {
                    if (score > 21 && score < minOver)
                        minOver = score;
                    else if (score <= 21 && score > maxUnder)
                        maxUnder = score;
                }

                return maxUnder == int.MinValue ? minOver : maxUnder;
            }

            private List<int> PossibleScores => throw new NotImplementedException();
            public bool Busted => Score() > 21;
            public bool Is21() => Score() == 21;
            public bool IsBlackJack => throw new NotImplementedException();
        }

        #endregion

        // 7.2 Call Center: Imagine you have a call center with three levels of employees: respondent, manager,
        // and director. An incoming telephone call must be first allocated to a respondent who is free. If the
        // respondent can't handle the call, he or she must escalate the call to a manager. If the manager is not
        //     free or not able to handle it, then the call should be escalated to a director. Design the classes and
        //     data structures for this problem. Implement a method dispatchCall() which assigns a call to
        //     the first available employee. 

        #region CallCenter

        public class Call
        {
            //minimal rank of employee who can handle this call
            private Rank _rank;

            //person who is calling
            private Caller _caller;

            //Employee who is handling call
            private Employee _handler;

            public Call(Caller c)
            {
                this._rank = Rank.Responder;
                _caller = c;
            }

            public void SetHandler(Employee e)
            {
                _handler = e;
            }

            public void Reply(string message)
            {
            }

            public Rank GetRank => _rank;
            public void SetRank(Rank r) => _rank = r;
            public Rank IncrementRank() => throw new NotImplementedException();

            public void Disconnect()
            {
            }
        }

        public abstract class Employee
        {
            private Call _currentCall = null;
            protected Rank _rank;

            public Employee(CallHandler handler)
            {
            }

            public void ReceiveCall(Call call)
            {
            }

            public void CallCompleted()
            {
            }

            public void EscalateAndReassign()
            {
            }

            public bool AssignNewCall() => throw new NotImplementedException();
            public bool IsFree => _currentCall == null;
            public Rank GetRank() => _rank;
        }

        public class Director : Employee
        {
            public Director(CallHandler handler) : base(handler)
            {
                _rank = Rank.Director;
            }
        }

        public class Manager : Employee
        {
            public Manager(CallHandler handler) : base(handler)
            {
                _rank = Rank.Manager;
            }
        }

        public class Respondent : Employee
        {
            public Respondent(CallHandler handler) : base(handler)
            {
                _rank = Rank.Responder;
            }
        }

        public class CallHandler
        {
            private int Levels = 3;

            private int Num_Respondents = 10;
            private int Num_Managers = 4;
            private int Num_Directors = 2;

            private List<List<Employee>> EmployeeLevels;

            private List<List<Call>> CallQueues;

            public CallHandler()
            {
            }

            public Employee GetHandlerForCall(Call call) => throw new NotImplementedException();

            public void DispatchCall(Caller caller)
            {
                Call call = new Call(caller);
                DispatchCall(call);
            }

            public void DispatchCall(Call call)
            {
                var emp = GetHandlerForCall(call);
                if (emp != null)
                {
                    emp.ReceiveCall(call);
                    call.SetHandler(emp);
                }
                else
                {
                    call.Reply("Please wait for free employee to reply");
                    CallQueues[(int) call.GetRank].Add(call);
                }
            }

            public bool AssignCall(Employee emp) => throw new NotImplementedException();
        }

        public class Caller
        {
        }

        public enum Rank
        {
            Responder,
            Director,
            Manager
        }

        #endregion

        // 7.3 Jukebox: Design a musical jukebox using object oriented principles. 

        #region JukeBox

        public class Jukebox
        {
            private CDPlayer _cdPlayer;
            private User _user;
            private List<CD> _cdCollection;
            private SongSelector _ts;

            public Jukebox(CDPlayer cdPlayer, User user, List<CD> cdCollection, SongSelector ts)
            {
                _cdPlayer = cdPlayer;
                _user = user;
                _cdCollection = cdCollection;
                _ts = ts;
            }

            public Song GetCurrentSong()
            {
                return _ts.GetCurrentSong();
            }

            public User User
            {
                set { _user = value; }
            }
        }

        public class CDPlayer
        {
            public PlayList PlayList { get; set; }
            public CD CD { get; set; }

            public CDPlayer(CD cd, PlayList playList)
            {
                this.CD = cd;
                this.PlayList = playList;
            }

            public CDPlayer(CD cd) : this(cd, null)
            {
            }

            public CDPlayer(PlayList p) : this((CD) null, p)
            {
            }

            public void PlaySong(Song s)
            {
            }
        }

        public class PlayList
        {
            private Song _currentSong;
            private Queue<Song> _queue;

            public PlayList(Song currentSong, Queue<Song> queue)
            {
                _currentSong = currentSong;
                _queue = queue;
            }

            public Song GetNextSToPlay() => _queue.Peek();
            public void QueueUpSong(Song s) => _queue.Enqueue(s);
        }

        public class CD
        {
        }

        public class Song
        {
        }

        public class User
        {
            public string Name { get; set; }
            public long ID { get; set; }

            public User(long id, string name)
            {
                ID = id;
                Name = name;
            }
        }

        public class SongSelector
        {
            public Song GetCurrentSong()
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        // 7.4 Parking Lot: Design a parking lot using object-oriented principles. 

        // 7.5 Online Book Reader: Design the data structures for an online book reader system. 

        // 7.6 Jigsaw: Implement an NxN jigsaw puzzle. Design the data structures and explain an algorithm to
        //     solve the puzzle. You can assume that you have a fi tsWi th method which, when passed two
        //     puzzle edges, returns true if the two edges belong together. 

        // 7 .7 Chat Server: Explain how you would design a chat server. In particular, provide details about the
        // various backend components, classes, and methods. What would be the hardest problems to solve? 

        // 7 .8 Othello: Othello is played as follows: Each Othello piece is white on one side and black on the other.
        //     When a piece is surrounded by its opponents on both the left and right sides, or both the top and
        //     bottom, it is said to be captured and its color is flipped. On your turn, you must capture at least one
        // of your opponent's pieces. The game ends when either user has no more valid moves. The win is
        // assigned to the person with the most pieces. Implement the object-oriented design for Othello. 

        // 7.9 Circular Array: Implement a CircularArray class that supports an array-like data structure which
        // can be efficiently rotated. If possible, the class should use a generic type (also called a template), and
        //     should support iteration via the standard for (Obj o : circularArray) notation. 
        public class CircularArray<T> : IEnumerable<T>
        {
            private readonly CircularArrayEnumerator<T> _enumerator;

            public CircularArray(T[] arr)
            {
                _enumerator = new CircularArrayEnumerator<T>(arr);
            }

            public IEnumerator<T> GetEnumerator() => _enumerator;

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public void Rotate(int steps) => _enumerator.Rotate(steps);
        }

        private class CircularArrayEnumerator<T> : IEnumerator<T>
        {
            private int _startIndex = 0;
            private int _iterationCount = 0;
            private readonly T[] _arr;

            public CircularArrayEnumerator(T[] arr)
            {
                _arr = arr;
            }

            public bool MoveNext()
            {
                if (_iterationCount == _arr.Length)
                    return false;
                _iterationCount++;
                return true;
            }

            public void Reset() => _iterationCount = 0;

            public T Current => _arr[(_startIndex + _iterationCount - 1) % _arr.Length];

            object? IEnumerator.Current => Current;

            public void Dispose() => Reset();

            public void Rotate(int steps)
            {
                _startIndex += (_startIndex + steps) % _arr.Length;
                _iterationCount = 0;
            }
        }

        // 7.1 O Minesweeper: Design and implement a text-based Minesweeper game. Minesweeper is the classic
        // single-player computer game where an NxN grid has B mines (or bombs) hidden across the grid. The
        // remaining cells are either blank or have a number behind them. The numbers reflect the number of
        // bombs in the surrounding eight cells. The user then uncovers a cell. If it is a bomb, the player loses.
        //     If it is a number, the number is exposed. If it is a blank cell, this cell and all adjacent blank cells (up to
        //     and including the surrounding numeric cells) are exposed. The player wins when all non-bomb cells
        // are exposed. The player can also flag certain places as potential bombs. This doesn't affect game
        // play, other than to block the user from accidentally clicking a cell that is thought to have a bomb.
        // (Tip for the reader: if you're not familiar with this game, please play a few rounds on line first.) 
        public class Minesweeper
        {
            private readonly int _bombsNumber;
            private readonly int _rowsNumber, _colsNumber;
            private int[,] _actualGrid;
            public char[,] _displayedGrid;
            private int _shootCount = 0;

            public Minesweeper(int bombsNumber, int n)
            {
                _bombsNumber = bombsNumber;
                this._rowsNumber = n;
                this._colsNumber = n;
                InitGrids();
            }

            private void InitGrids()
            {
                _actualGrid = new int[_rowsNumber, _colsNumber];
                _displayedGrid = new char[_rowsNumber, _colsNumber];
                var rand = new Random();
                for (int i = 0; i < _bombsNumber; i++)
                {
                    int row, col;
                    do
                    {
                        row = rand.Next(0, _rowsNumber - 1);
                        col = rand.Next(0, _colsNumber - 1);
                    } while (_actualGrid[row, col] == -1);

                    _actualGrid[row, col] = -1;
                    if (IsNotBomb(row - 1, col - 1))
                        _actualGrid[row - 1, col - 1]++;
                    if (IsNotBomb(row - 1, col))
                        _actualGrid[row - 1, col]++;
                    if (IsNotBomb(row - 1, col + 1))
                        _actualGrid[row - 1, col + 1]++;
                    if (IsNotBomb(row, col - 1))
                        _actualGrid[row, col - 1]++;
                    if (IsNotBomb(row, col + 1))
                        _actualGrid[row, col + 1]++;
                    if (IsNotBomb(row + 1, col))
                        _actualGrid[row + 1, col]++;
                    if (IsNotBomb(row + 1, col + 1))
                        _actualGrid[row + 1, col + 1]++;
                    if (IsNotBomb(row + 1, col - 1))
                        _actualGrid[row + 1, col - 1]++;
                }

                for (int i = 0; i < _rowsNumber; i++)
                for (int j = 0; j < _colsNumber; j++)
                    _displayedGrid[i, j] = '?';

                bool IsNotBomb(int r, int c) =>
                    r >= 0 && c >= 0 && r < _rowsNumber && c < _colsNumber && _actualGrid[r, c] != -1;
            }

            public void Reset()
            {
                InitGrids();
                _shootCount = 0;
            }

            public void Shoot(int row, int column)
            {
                if (_actualGrid[row, column] == -1)
                {
                    Lose();
                    return;
                }

                Dfs(row, column, new HashSet<(int, int)>());
                
                _shootCount++;
                if(_shootCount == _rowsNumber * _colsNumber - _bombsNumber)
                    Won();
            }

            private void Dfs(int r, int c, HashSet<(int,int)> set)
            {
                if (r < 0 || c < 0 || r >= _rowsNumber || c >= _colsNumber || set.Contains((r,c)))
                    return;
                set.Add((r, c));
                if (_actualGrid[r, c] == 0)
                {
                    _displayedGrid[r, c] = ' ';
                    Dfs(r - 1, c, set);
                    Dfs(r - 1, c - 1, set);
                    Dfs(r - 1, c + 1, set);
                    Dfs(r, c - 1, set);
                    Dfs(r, c + 1, set);
                    Dfs(r + 1, c, set);
                    Dfs(r + 1, c - 1, set);
                    Dfs(r + 1, c + 1, set);
                }
                else _displayedGrid[r,c] = _actualGrid[r, c].ToString()[0];
            }
            
            private void Lose(){}
            private void Won(){}
        }
        
        // 7.11 File System: Explain the data structures and algorithms that you would use to design an in-memory
        //     file system. Illustrate with an example in code where possible. 
        
        // 7.12 Hash Table: Design and implement a hash table which uses chaining (linked lists) to handle
        // collisions. 
    }
}