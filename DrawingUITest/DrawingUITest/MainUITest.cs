using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace DrawingUITest
{
    /// <summary>
    /// CodedUITest1 的摘要說明
    /// </summary>
    [CodedUITest]
    public class MainUITest
    {
        enum ShapeType
        {
            Line, 
            Rectangle, 
            SixSide
        }

        const string FILE_PATH = @"../../../DrawingForm.exe";
        private const string FORM_TITLE = "DrawingForm";

        // 初始化 test
        [TestInitialize()]
        public void Initialize()
        {
            Robot.Initialize(FILE_PATH, FORM_TITLE);
        }

        // 清除 test
        [TestCleanup()]
        public void Cleanup()
        {
            Robot.CleanUp();
        }

        // 測試畫 rectangle
        [TestMethod()]
        public void DrawRectangleTest()
        {
            DrawRectangle("canvas", 100, 100, 200, 200);
            Click("canvas", 150, 150);
            Robot.AssertText("selectLabel", "Selected : Rectangle (100, 100, 100, 100)");
        }

        // 測試畫 line
        [TestMethod()]
        public void DrawLineTest()
        {
            DrawLine("canvas", 100, 100, 200, 200);
            Click("canvas", 150, 150);
            Robot.AssertText("selectLabel", "Selected : Line (100, 100, 100, 100)");
        }

        // 測試畫 hexagon
        [TestMethod()]
        public void DrawSixSideTest()
        {
            DrawSixSide("canvas", 100, 100, 200, 200);
            Click("canvas", 150, 150);
            Robot.AssertText("selectLabel", "Selected : Hexagon (100, 100, 100, 100)");
        }

        // 測試畫 rectangle 後清除
        [TestMethod()]
        public void ClearTest()
        {
            DrawRectangle("canvas", 100, 100, 200, 200);
            ClickButton("Clear");
            Robot.AssertText("selectLabel", "Selected : ");
        }

        // 測試畫一間屋子
        [TestMethod()]
        public void HouseTest()
        {
            DrawAndClick("canvas", ShapeType.Line, 400, 100, 250, 200);
            Robot.AssertText("selectLabel", "Selected : Line (250, 100, 150, 100)");
            DrawAndClick("canvas", ShapeType.Line, 400, 100, 550, 200);
            Robot.AssertText("selectLabel", "Selected : Line (400, 100, 150, 100)");
            DrawAndClick("canvas", ShapeType.Rectangle, 250, 200, 550, 300);
            Robot.AssertText("selectLabel", "Selected : Rectangle (250, 200, 300, 100)");
            DrawAndClick("canvas", ShapeType.Line, 300, 110, 325, 150);
            Robot.AssertText("selectLabel", "Selected : Line (300, 110, 25, 40)");
            DrawAndClick("canvas", ShapeType.Line, 500, 110, 475, 150);
            Robot.AssertText("selectLabel", "Selected : Line (475, 110, 25, 40)");
            DrawAndClick("canvas", ShapeType.SixSide, 300, 210, 330, 240);
            Robot.AssertText("selectLabel", "Selected : Hexagon (300, 210, 30, 30)");
            DrawAndClick("canvas", ShapeType.SixSide, 410, 220, 440, 250);
            Robot.AssertText("selectLabel", "Selected : Hexagon (410, 220, 30, 30)");
            DrawAndClick("canvas", ShapeType.Rectangle, 350, 280, 500, 290);
            Robot.AssertText("selectLabel", "Selected : Rectangle (350, 280, 150, 10)");
            DrawAndClick("canvas", ShapeType.Rectangle, 480, 205, 490, 255);
            Robot.AssertText("selectLabel", "Selected : Rectangle (480, 205, 10, 50)");
            DrawAndClick("canvas", ShapeType.Rectangle, 500, 215, 510, 260);
            Robot.AssertText("selectLabel", "Selected : Rectangle (500, 215, 10, 45)");
            DrawAndClick("canvas", ShapeType.Rectangle, 520, 210, 530, 275);
            Robot.AssertText("selectLabel", "Selected : Rectangle (520, 210, 10, 65)");
            DrawAndClick("canvas", ShapeType.Rectangle, 550, 80, 570, 220);
            Robot.AssertText("selectLabel", "Selected : Rectangle (550, 80, 20, 140)");
            ClickButton("Clear");
        }

        // name 為panel AccessibleName
        // 畫 rectangle
        public static void DrawRectangle(string name, int x1, int y1, int x2, int y2)
        {
            ClickButton("Rectangle");
            UITestControl canvas = Robot.FindPanel(name);
            Mouse.StartDragging(canvas, new Point(x1, y1));
            Mouse.StopDragging(canvas, new Point(x2, y2));
        }

        // 畫 line
        public static void DrawLine(string name, int x1, int y1, int x2, int y2)
        {
            ClickButton("Line");
            UITestControl canvas = Robot.FindPanel(name);
            Mouse.StartDragging(canvas, new Point(x1, y1));
            Mouse.StopDragging(canvas, new Point(x2, y2));
        }

        // 畫 hexagon
        public static void DrawSixSide(string name, int x1, int y1, int x2, int y2)
        {
            ClickButton("Hexagon");
            UITestControl canvas = Robot.FindPanel(name);
            Mouse.StartDragging(canvas, new Point(x1, y1));
            Mouse.StopDragging(canvas, new Point(x2, y2));
        }

        //Using Robot to ClickButton
        private static void ClickButton(string buttonText)
        {
            Robot.ClickButton(buttonText);
        }

        // 點擊 canvas
        private static void Click(string name, int x, int y)
        {
            UITestControl canvas = Robot.FindPanel(name);
            Mouse.Click(canvas, new Point(x, y));
        }

        // 畫加上 click
        private static void DrawAndClick(string name, ShapeType shapeType, int x1, int y1, int x2, int y2)
        {
            switch (shapeType)
            {
                case ShapeType.Line:
                    DrawLine(name, x1, y1, x2, y2);
                    break;
                case ShapeType.Rectangle:
                    DrawRectangle(name, x1, y1, x2, y2);
                    break;
                case ShapeType.SixSide:
                    DrawSixSide(name, x1, y1, x2, y2);
                    break;
            }
            Click(name, x1, y1);
        }
    }
}
