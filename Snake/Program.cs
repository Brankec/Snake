using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Snake
{
    struct Snake
    {
        public float x, y;
    }

    struct Fruct
    {
        public float x, y;
    }

    class Program
    {
        static uint N = 30, M = 20;
        static uint size = 16;
        static uint w = size * N;
        static uint h = size * M;

        static int dir, num = 4;

        static Random random = new Random();

        static void Main(string[] args)
        {
            RenderWindow window = new RenderWindow(new VideoMode(w, h), "Snake");

            Texture t1, t2;
            t1 = new Texture("C:/Users/Gejmer/source/repos/Snake/Snake/images/white.png");
            t2 = new Texture("C:/Users/Gejmer/source/repos/Snake/Snake/images/red.png");

            Sprite sprite1 = new Sprite(t1);
            Sprite sprite2 = new Sprite(t2);

            Clock clock = new Clock();
            float timer = 0, delay = 0.1f;

            Fruct f = new Fruct();
            Snake[] s = new Snake[100];
            for(int i = 0; i < s.Length; i++)
            {
                s[i] = new Snake();
            }

            f.x = 10;
            f.y = 10;

            while (window.IsOpen)
            {
                float time = clock.ElapsedTime.AsSeconds();
                clock.Restart();
                timer += time;

                if (Keyboard.IsKeyPressed(Keyboard.Key.A)) dir = 1;
                if (Keyboard.IsKeyPressed(Keyboard.Key.D)) dir = 2;
                if (Keyboard.IsKeyPressed(Keyboard.Key.W)) dir = 3;
                if (Keyboard.IsKeyPressed(Keyboard.Key.S)) dir = 0;

                if (timer > delay) { timer = 0; Tick(ref s, ref f); }

                ////// draw  ///////
                window.Clear();

                for (int i = 0; i < N; i++)
                    for (int j = 0; j < M; j++)
                    {
                        sprite1.Position = new Vector2f(i * size, j * size);
                        window.Draw(sprite1);
                    }

                for (int i = 0; i < num; i++)
                {
                    sprite2.Position = new Vector2f(s[i].x * size, s[i].y * size);
                    window.Draw(sprite2);
                }

                sprite2.Position = new Vector2f(f.x * size, f.y * size);
                window.Draw(sprite2);

                window.Display();
            }
        }

        static void Tick(ref Snake[] s, ref Fruct f)
        {
            for (int i = num; i > 0; --i)
            {
                s[i].x = s[i - 1].x;
                s[i].y = s[i - 1].y;
            }

            if (dir == 0) s[0].y += 1;
            if (dir == 1) s[0].x -= 1;
            if (dir == 2) s[0].x += 1;
            if (dir == 3) s[0].y -= 1;

            if ((s[0].x == f.x) && (s[0].y == f.y))
            {
                num++;
                f.x = random.Next() % N;
                f.y = random.Next() % M;
            }

            if (s[0].x > N) s[0].x = 0; if (s[0].x < 0) s[0].x = N;
            if (s[0].y > M) s[0].y = 0; if (s[0].y < 0) s[0].y = M;

            for (int i = 1; i < num; i++)
                if (s[0].x == s[i].x && s[0].y == s[i].y) num = i;
        }
    }
}
