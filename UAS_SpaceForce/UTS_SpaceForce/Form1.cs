using System.Media;
using WMPLib;



namespace UTS_SpaceForce
{

    public partial class Form1 : Form
    {
        // Player
        private PictureBox Player;
        private List<PictureBox> playerBullets = new List<PictureBox>();
        private int playerHP = 100;
        private int playerSpeed = 20;
        int bulletSpeed = 10;
        private bool goLeft, goRight, goUp, goDown;
        private ProgressBar playerHealthBar = new ProgressBar();

        // Enemy
        private List<Enemy> enemies = new List<Enemy>();
        private int enemySpeed = 3;
        private Image enemyImage;
        private bool isSpawningEnemies = false;
        private int enemiesToSpawn = 0;
        private int enemySpawnDelay = 100;
        private int enemySpawnCounter = 0;

        // Enemy Bullet
        private List<PictureBox> enemyBullets = new List<PictureBox>();
        private int enemyBulletSpeed = 6;

        // Boss
        private bool isBossWave = false;
        private bool bossActive = false;
        private Enemy bossEnemy;
        private ProgressBar bossHPBar;
        private System.Windows.Forms.Timer bossShootTimer;
        private System.Windows.Forms.Timer bossMoveTimer = new System.Windows.Forms.Timer();
        private int bossSpeed = 3;
        private bool movingRight = true;
        private Dictionary<PictureBox, Point> bossBulletDirections = new Dictionary<PictureBox, Point>();

        // Score & Coin
        private int score = 0;
        private int coins = 0;
        private int lastCoinScore = 0;

        // Wave
        private int waveNumber = 1;
        private int spawnCounter = 0;

        // Shooting System
        private DateTime lastShotTime = DateTime.MinValue;
        private TimeSpan shootCooldown = TimeSpan.FromMilliseconds(300); // ms antar tembakan

        // Misc
        private Random rand = new Random();

        // Music
        SoundPlayer bgmPlayer;
        bool isBgmPlaying = false;
        SoundPlayer defeatTheme = new SoundPlayer("Assets/defeatTheme.wav");



        // Enemy
        public class Enemy
        {
            public PictureBox Sprite { get; set; }
            public int HP { get; set; } = 100;
        }



        public Form1()
        {
            InitializeComponent();
            ShowLobby();
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
            gameTimer.Tick += gameTimer_Tick;
            gameTimer.Interval = 20;
            gameTimer.Start();
            enemyImage = Image.FromFile("Assets/enemy.png");
        }

        private void ShowLobby()
        {
            panelLobby.Visible = true;
            string bgmPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "menuTheme.wav");
            if (File.Exists(bgmPath))
            {
                bgmPlayer = new System.Media.SoundPlayer(bgmPath);
                bgmPlayer.PlayLooping();
            }
        }
        private void ShowPanel(Panel targetPanel)
        {
            panelLobby.Visible = false;
            panelGame.Visible = false;
            panelPause.Visible = false;

            targetPanel.Visible = true;
            targetPanel.BringToFront();

            // BGM

            if (bgmPlayer != null)
            {
                bgmPlayer.Stop();
                bgmPlayer.Dispose();
                bgmPlayer = null;
                isBgmPlaying = false;
            }

            string bgmPath = "";

            if (targetPanel == panelLobby)
            {
                bgmPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "menuTheme.wav");
            }
            else if (targetPanel == panelPause)
            {
                bgmPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "gameTheme.wav");
            }
            else if (targetPanel == panelGame)
            {
                bgmPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "pauseTheme.wav");
            }

            if (File.Exists(bgmPath))
            {
                bgmPlayer = new SoundPlayer(bgmPath);
                bgmPlayer.PlayLooping();
                isBgmPlaying = true;
            }

            if (targetPanel == panelGame)
            {
                panelGame.Focus();
            }
        }

        private void PlaySoundEffect(string soundPath)
        {
            WindowsMediaPlayer effectPlayer = new WindowsMediaPlayer();
            effectPlayer.URL = soundPath;
            effectPlayer.settings.volume = 100;
            effectPlayer.controls.play();
        }


        private void ShowGameOver()
        {
            gameTimer.Stop();
            defeatTheme.Play();
            defeatTheme.PlayLooping();
            FinalScoreTx.Text = score.ToString();
            MessageBox.Show("Final Score: " + score, "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
            defeatTheme.Stop();
            ShowPanel(panelLobby);
            ResetGame();
        }

        private void ShowTutorial()
        {
            MessageBox.Show("Up = Maju\nDown = Mundur\nRight = Kanan\nLeft = Kiri\nSpace = Tembak", "Tutorial Game");
            ShowPanel(panelLobby);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ShowPanel(panelGame);
            InitializeGame();
        }


        private void btnTutorial_Click(object sender, EventArgs e)
        {
            ShowTutorial();
        }

        private void btnBackToLobby_Click(object sender, EventArgs e)
        {
            ResetGame();
            ShowPanel(panelLobby);
        }


        private void btnPause_Click(object sender, EventArgs e)
        {
            ShowPanel(panelPause);
            gameTimer.Stop();
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            ShowPanel(panelGame);
            gameTimer.Start();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // reset game

        private void ResetGame()
        {
            if (bossShootTimer != null)
            {
                bossShootTimer.Stop();
                bossShootTimer.Dispose();
                bossShootTimer = null;
            }

            if (bossMoveTimer != null)
            {
                bossMoveTimer.Stop();
                bossMoveTimer.Dispose();
                bossMoveTimer = null;
            }

            for (int i = panelGame.Controls.Count - 1; i >= 0; i--)
            {
                Control ctrl = panelGame.Controls[i];
                if (ctrl.Tag != null && ctrl.Tag.ToString() == "bossBullet")
                {
                    panelGame.Controls.Remove(ctrl);
                    ctrl.Dispose();
                }
            }
            bossBulletDirections.Clear();

            if (bossHPBar != null)
            {
                panelGame.Controls.Remove(bossHPBar);
                bossHPBar.Dispose();
                bossHPBar = null;
            }

            if (bossEnemy != null && bossEnemy.Sprite != null)
            {
                if (panelGame.Controls.Contains(bossEnemy.Sprite))
                    panelGame.Controls.Remove(bossEnemy.Sprite);
                bossEnemy.Sprite.Dispose();
            }

            bossEnemy = null;
            bossActive = false;
            isBossWave = false;

            foreach (var enemy in enemies)
            {
                if (enemy.Sprite != null)
                {
                    panelGame.Controls.Remove(enemy.Sprite);
                    enemy.Sprite.Dispose();
                }
            }
            enemies.Clear();

            foreach (var bullet in enemyBullets)
            {
                panelGame.Controls.Remove(bullet);
                bullet.Dispose();
            }
            enemyBullets.Clear();

            foreach (var bullet in playerBullets)
            {
                panelGame.Controls.Remove(bullet);
                bullet.Dispose();
            }
            playerBullets.Clear();

            if (Player != null && panelGame.Controls.Contains(Player))
            {
                panelGame.Controls.Remove(Player);
                Player.Dispose();
                Player = null;
            }

            playerHP = 100;
            if (playerHealthBar != null)
                playerHealthBar.Value = 100;

            spawnCounter = 0;
            waveNumber = 1;
            score = 0;
            coins = 0;

            ScoreTx.Text = "0";
            FinalScoreTx.Text = "0";
            CoinTx.Text = "0";
            FinalCoinTx.Text = "0";
        }

        //Game Panel

        private void InitializeGame()
        {
            Player = new PictureBox
            {
                Size = new Size(70, 70),
                Image = Properties.Resources.Player,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(panelGame.Width / 2, panelGame.Height - 60),
                BackColor = Color.Transparent
            };
            panelGame.Controls.Add(Player);
            gameTimer.Start();

            playerHP = 100;

            playerHealthBar.Maximum = 100;
            playerHealthBar.Value = playerHP;
            playerHealthBar.Size = new Size(200, 20);
            playerHealthBar.Location = new Point(70, 15);
            playerHealthBar.ForeColor = Color.Green;

            panelGame.Controls.Add(playerHealthBar);

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            MovePlayer();
            UpdateBullets();
            MoveEnemies();
            UpdateEnemyBullets();
            CheckCollisions();
            RemoveOffscreenEnemies();
            HandleEnemyWave();
            panelGame.Invalidate();
            MoveBossBullets();
            HandleEnemyWave();
        }

        private void MovePlayer()
        {
            if (Player == null)
                return;

            if (goLeft && Player.Left > 0)
            {
                Player.Left -= playerSpeed;
            }
            if (goRight && Player.Right < this.ClientSize.Width)
            {
                Player.Left += playerSpeed;
            }
            if (goUp && Player.Top > 0)
            {
                Player.Top -= playerSpeed;
            }
            if (goDown && Player.Bottom < this.ClientSize.Height)
            {
                Player.Top += playerSpeed;
            }
        }



        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) goLeft = true;
            if (e.KeyCode == Keys.Right) goRight = true;
            if (e.KeyCode == Keys.Up) goUp = true;
            if (e.KeyCode == Keys.Down) goDown = true;
            if (e.KeyCode == Keys.Space) ShootPlayerBullet();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) goLeft = false;
            if (e.KeyCode == Keys.Right) goRight = false;
            if (e.KeyCode == Keys.Up) goUp = false;
            if (e.KeyCode == Keys.Down) goDown = false;
        }

        private void BtnBuyRepair_Click(object sender, EventArgs e)
        {
            int cost = 50;
            int healAmount = 100;
            int maxHP = 100;

            if (playerHP >= maxHP)
            {
                MessageBox.Show("HP kamu sudah penuh!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (coins < cost)
            {
                MessageBox.Show("Koin tidak cukup untuk membeli Repair Kit!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            coins -= cost;
            playerHP = Math.Min(playerHP + healAmount, maxHP);
            playerHealthBar.Value = playerHP;

            CoinTx.Text = coins.ToString();
            FinalCoinTx.Text = coins.ToString();

            string repairPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "repair.wav");
            PlaySoundEffect(repairPath);


            MessageBox.Show("Repair Kit berhasil digunakan!", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void RemoveOffscreenEnemies()
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i].Sprite.Top > panelGame.Height || enemies[i].HP <= 0)
                {
                    panelGame.Controls.Remove(enemies[i].Sprite);
                    enemies[i].Sprite.Dispose();
                    enemies.RemoveAt(i);
                }
            }
        }

        private void ShootPlayerBullet()
        {
            if (DateTime.Now - lastShotTime < shootCooldown)
                return;
            if (Player == null)
                return;

            lastShotTime = DateTime.Now;
            PictureBox bullet = new PictureBox
            {
                Size = new Size(5, 5),
                BackColor = Color.Yellow,
                Location = new Point(Player.Left + Player.Width / 2 - 2, Player.Top),
                Tag = "playerBullet"
            };
            playerBullets.Add(bullet);
            panelGame.Controls.Add(bullet);

            string shootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "shoot.wav");
            PlaySoundEffect(shootPath);

        }


        private void UpdateBullets()
        {
            for (int i = playerBullets.Count - 1; i >= 0; i--)
            {
                var b = playerBullets[i];
                b.Top -= bulletSpeed;
                if (b.Top < 0)
                {
                    panelGame.Controls.Remove(b);
                    playerBullets.RemoveAt(i);
                }
            }
        }

        //Enemies

        private void HandleEnemyWave()
        {
            if (!isSpawningEnemies && enemies.Count == 0 && !bossActive)
            {
                waveNumber++;
                if (waveNumber % 5 == 0) // setiap kelipatan 5 → BOS
                {
                    isBossWave = true;
                    SpawnBoss();
                }
                else
                {
                    enemiesToSpawn = 5;
                    enemySpawnCounter = 0;
                    isSpawningEnemies = true;
                }
            }

            if (isSpawningEnemies && enemiesToSpawn > 0)
            {
                enemySpawnCounter++;
                if (enemySpawnCounter >= enemySpawnDelay)
                {
                    enemySpawnCounter = 0;
                    SpawnOneEnemy();
                    enemiesToSpawn--;

                    if (enemiesToSpawn <= 0)
                    {
                        isSpawningEnemies = false;
                    }
                }
            }
        }

        private void SpawnOneEnemy()
        {
            PictureBox enemySprite = new PictureBox
            {
                Size = new Size(70, 70),
                Image = enemyImage,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(rand.Next(0, panelGame.Width - 70), -70),
                BackColor = Color.Transparent,
                Tag = "enemy"
            };

            Enemy enemy = new Enemy
            {
                Sprite = enemySprite,
                HP = 100
            };

            enemies.Add(enemy);
            panelGame.Controls.Add(enemySprite);
        }

        private void MoveEnemies()
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                PictureBox enemy = enemies[i].Sprite;

                if (enemies[i] == bossEnemy)
                    continue;

                enemy.Top += enemySpeed;

                if (rand.Next(0, 100) < 5) ShootEnemyBullet(enemy);

                if (enemy.Top > panelGame.Height)
                {
                    panelGame.Controls.Remove(enemy);
                    enemies.RemoveAt(i);
                }
            }
        }

        private void ShootEnemyBullet(PictureBox enemy)
        {
            PictureBox bullet = new PictureBox
            {
                Size = new Size(5, 10),
                BackColor = Color.Red,
                Location = new Point(enemy.Left + enemy.Width / 2 - 2, enemy.Bottom),
                Tag = "enemyBullet"
            };
            enemyBullets.Add(bullet);
            panelGame.Controls.Add(bullet);
        }

        private void UpdateEnemyBullets()
        {
            for (int i = enemyBullets.Count - 1; i >= 0; i--)
            {
                var b = enemyBullets[i];
                b.Top += enemyBulletSpeed;
                if (b.Top > panelGame.Height)
                {
                    panelGame.Controls.Remove(b);
                    enemyBullets.RemoveAt(i);
                }
            }
        }

        // Boss

        private void SpawnBoss()
        {
            PictureBox bossSprite = new PictureBox
            {
                Size = new Size(150, 150),
                Image = Image.FromFile("Assets/boss.png"),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(panelGame.Width / 2 - 75, 50),
                BackColor = Color.Transparent,
                Tag = "boss"
            };

            bossEnemy = new Enemy
            {
                Sprite = bossSprite,
                HP = 500
            };

            bossActive = true;
            panelGame.Controls.Add(bossSprite);

            //  HP Bar
            bossHPBar = new ProgressBar
            {
                Maximum = bossEnemy.HP,
                Value = bossEnemy.HP,
                Width = 200,
                Height = 20,
                Location = new Point(panelGame.Width / 2 - 100, 10),
                ForeColor = Color.Red
            };
            panelGame.Controls.Add(bossHPBar);

            // tembakan bos
            if (bossShootTimer != null)
            {
                bossShootTimer.Stop();
                bossShootTimer.Dispose();
            }

            bossShootTimer = new System.Windows.Forms.Timer();
            bossShootTimer.Interval = 1000;
            bossShootTimer.Tick += BossShootTimer_Tick;
            bossShootTimer.Start();


            // pergerakan Boss
            if (bossMoveTimer != null)
            {
                bossMoveTimer.Stop();
                bossMoveTimer.Dispose();
            }

            bossMoveTimer = new System.Windows.Forms.Timer();
            bossMoveTimer.Interval = 30;
            bossMoveTimer.Tick += BossMoveTimer_Tick;
            bossMoveTimer.Start();

        }

        private void BossShootTimer_Tick(object sender, EventArgs e)
        {
            if (!bossActive) return;

            PictureBox bullet = new PictureBox
            {
                Size = new Size(10, 10),
                BackColor = Color.Orange,
                Location = new Point(
                bossEnemy.Sprite.Left + bossEnemy.Sprite.Width / 2, bossEnemy.Sprite.Bottom),
                Tag = "bossBullet"
            };
            enemyBullets.Add(bullet);
            panelGame.Controls.Add(bullet);
            bossBulletDirections[bullet] = new Point(rand.Next(-5, 6), 10);
        }

        private void BossMoveTimer_Tick(object sender, EventArgs e)
        {
            if (bossEnemy == null || bossEnemy.Sprite == null) return;

            PictureBox boss = bossEnemy.Sprite;

            if (movingRight)
            {
                boss.Left += bossSpeed;
                if (boss.Right >= panelGame.Width - 200)
                {
                    movingRight = false;
                }
            }
            else
            {
                boss.Left -= bossSpeed;
                if (boss.Left <= 200)
                {
                    movingRight = true;
                }
            }
        }

        private void MoveBossBullets()
        {
            List<PictureBox> bulletsToRemove = new List<PictureBox>();

            foreach (Control ctrl in panelGame.Controls)
            {
                if (ctrl is PictureBox bullet && bullet.Tag != null && bullet.Tag.ToString() == "bossBullet")
                {
                    if (!bossBulletDirections.ContainsKey(bullet))
                        continue;

                    Point dir = bossBulletDirections[bullet];
                    bullet.Left += dir.X;
                    bullet.Top += dir.Y;


                    if (bullet.Top > panelGame.Height || bullet.Left < 0 || bullet.Right > panelGame.Width)
                    {
                        bulletsToRemove.Add(bullet);
                    }
                }
            }

            foreach (var bullet in bulletsToRemove)
            {
                panelGame.Controls.Remove(bullet);
                bossBulletDirections.Remove(bullet);
                bullet.Dispose();
            }
        }

        // Dead

        private void CheckCollisions()
        {
            // PLAYER BULLETS vs ENEMIES (NORMAL & BOSS) 
            for (int i = playerBullets.Count - 1; i >= 0; i--)
            {
                PictureBox bullet = playerBullets[i];
                bool bulletHit = false;

                for (int j = enemies.Count - 1; j >= 0; j--)
                {
                    Enemy enemy = enemies[j];
                    if (enemy == null || enemy.Sprite == null) continue;

                    if (bullet.Bounds.IntersectsWith(enemy.Sprite.Bounds))
                    {
                        enemy.HP -= 25;
                        panelGame.Controls.Remove(bullet);
                        playerBullets.RemoveAt(i);
                        bulletHit = true;

                        if (enemy.HP <= 0)
                        {
                            string deadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Explosion.wav");
                            PlaySoundEffect(deadPath);


                            PictureBox explosion = new PictureBox
                            {
                                Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Explosive.gif")),
                                Size = new Size(100, 100),
                                SizeMode = PictureBoxSizeMode.StretchImage,
                                BackColor = Color.Transparent,
                                Location = enemy.Sprite.Location
                            };

                            panelGame.Controls.Add(explosion);
                            explosion.BringToFront();

                            var explosionTimer = new System.Windows.Forms.Timer();
                            explosionTimer.Interval = 500;
                            explosionTimer.Tick += (s, args) =>
                            {
                                explosionTimer.Stop();
                                explosionTimer.Dispose();
                                panelGame.Controls.Remove(explosion);
                                explosion.Dispose();
                            };
                            explosionTimer.Start();

                            panelGame.Controls.Remove(enemy.Sprite);
                            enemies.RemoveAt(j);

                            score += 100;
                            coins += 10;

                            ScoreTx.Text = score.ToString();
                            FinalScoreTx.Text = score.ToString();
                            CoinTx.Text = coins.ToString();
                            FinalCoinTx.Text = coins.ToString();

                        }

                        break;
                    }
                }

                if (!bulletHit && bossActive && bossEnemy != null && bossEnemy.Sprite != null)
                {
                    if (bullet.Bounds.IntersectsWith(bossEnemy.Sprite.Bounds))
                    {
                        bossEnemy.HP -= 25;
                        panelGame.Controls.Remove(bullet);
                        playerBullets.RemoveAt(i);

                        if (bossHPBar != null)
                            bossHPBar.Value = Math.Max(0, bossEnemy.HP);

                        if (bossEnemy.HP <= 0)
                        {
                            var bossSprite = bossEnemy.Sprite;

                            string deadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Explosion.wav");
                            PlaySoundEffect(deadPath);

                            PictureBox bossExplosion = new PictureBox
                            {
                                Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Explosive.gif")),
                                Size = bossSprite.Size,
                                Location = bossSprite.Location,
                                BackColor = Color.Transparent,
                                SizeMode = PictureBoxSizeMode.StretchImage
                            };
                            panelGame.Controls.Add(bossExplosion);
                            bossExplosion.BringToFront();

                            System.Windows.Forms.Timer explosionTimer = new System.Windows.Forms.Timer();
                            explosionTimer.Interval = 800;
                            explosionTimer.Tick += (s, e) =>
                            {
                                explosionTimer.Stop();
                                explosionTimer.Dispose();
                                panelGame.Controls.Remove(bossExplosion);
                                bossExplosion.Dispose();
                            };
                            explosionTimer.Start();

                            score += 500;
                            ScoreTx.Text = score.ToString();
                            FinalScoreTx.Text = score.ToString();

                            bossShootTimer?.Stop();
                            bossShootTimer?.Dispose();
                            bossShootTimer = null;

                            bossMoveTimer?.Stop();
                            bossMoveTimer?.Dispose();
                            bossMoveTimer = null;

                            bossActive = false;
                            isBossWave = false;
                            waveNumber++;

                            if (panelGame.Controls.Contains(bossSprite))
                                panelGame.Controls.Remove(bossSprite);
                            bossSprite.Dispose();

                            if (panelGame.Controls.Contains(bossHPBar))
                                panelGame.Controls.Remove(bossHPBar);
                            bossHPBar?.Dispose();
                            bossHPBar = null;

                            bossEnemy = null;

                            score += 500;
                            coins += 50;

                            ScoreTx.Text = score.ToString();
                            FinalScoreTx.Text = score.ToString();
                            CoinTx.Text = coins.ToString();
                            FinalCoinTx.Text = coins.ToString();


                            break;
                        }
                    }
                }


            }

            // ENEMY BULLETS vs PLAYER 
            for (int i = enemyBullets.Count - 1; i >= 0; i--)
            {
                if (i >= enemyBullets.Count || Player == null) continue;

                PictureBox bullet = enemyBullets[i];
                if (bullet.Bounds.IntersectsWith(Player.Bounds))
                {
                    int damage = (bullet.Tag != null && bullet.Tag.ToString() == "bossBullet") ? 10 : 5;
                    playerHP -= damage;
                    playerHealthBar.Value = Math.Max(0, playerHP);

                    panelGame.Controls.Remove(bullet);
                    enemyBullets.RemoveAt(i);

                    if (playerHP <= 0)
                    {
                        HandlePlayerDeath();
                    }
                }
            }

            // ENEMY COLLISION with PLAYER 
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (Player != null && enemies[i].Sprite.Bounds.IntersectsWith(Player.Bounds))
                {
                    HandlePlayerEnemyCollision(enemies[i]);
                    enemies.RemoveAt(i);
                    break;
                }
            }
        }

        private void HandlePlayerDeath()
        {
            string deadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Explosion.wav");
            PlaySoundEffect(deadPath);

            PictureBox explosion = new PictureBox
            {
                Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Explosive.gif")),
                Size = new Size(100, 100),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
                Location = Player.Location
            };

            panelGame.Controls.Add(explosion);
            explosion.BringToFront();

            panelGame.Controls.Remove(Player);
            Player.Dispose();
            Player = null;

            var explosionTimer = new System.Windows.Forms.Timer();
            explosionTimer.Interval = 500;
            explosionTimer.Tick += (s, args) =>
            {
                explosionTimer.Stop();
                panelGame.Controls.Remove(explosion);
                explosion.Dispose();
                explosionTimer.Dispose();
                ShowGameOver();
            };
            explosionTimer.Start();
        }

        private void HandlePlayerEnemyCollision(Enemy enemy)
        {
            string deadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Explosion.wav");
            PlaySoundEffect(deadPath);

            PictureBox explosionEnemy = new PictureBox
            {
                Size = enemy.Sprite.Size,
                Location = enemy.Sprite.Location,
                Image = Image.FromFile("Assets/Explosive.gif"),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            panelGame.Controls.Add(explosionEnemy);
            explosionEnemy.BringToFront();

            PictureBox explosionPlayer = new PictureBox
            {
                Size = Player.Size,
                Location = Player.Location,
                Image = Image.FromFile("Assets/Explosive.gif"),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            panelGame.Controls.Add(explosionPlayer);
            explosionPlayer.BringToFront();

            playerHP = 0;
            playerHealthBar.Value = 0;

            panelGame.Controls.Remove(enemy.Sprite);
            panelGame.Controls.Remove(Player);
            Player.Dispose();
            Player = null;

            var explosionTimer = new System.Windows.Forms.Timer();
            explosionTimer.Interval = 500;
            explosionTimer.Tick += (s, args) =>
            {
                explosionTimer.Stop();
                panelGame.Controls.Remove(explosionEnemy);
                explosionEnemy.Dispose();
                panelGame.Controls.Remove(explosionPlayer);
                explosionPlayer.Dispose();
                explosionTimer.Dispose();
                ShowGameOver();
            };
            explosionTimer.Start();
        }


    }
}
