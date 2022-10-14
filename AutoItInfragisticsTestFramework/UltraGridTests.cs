using AutoIt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pages;
using System;
using System.IO;

namespace AutoItInfragisticsTestFramework
{
    [TestClass]
    public class UltraGridTests
    {
        private const string APP_TO_TEST = "InfragisticsDemoV1.exe";
        private IntPtr _window;
        private int _timeout = 3;
        private MainScreen _mainScreen;

        [TestInitialize]
        public void Initialize()
        {
            _mainScreen = new MainScreen();
            AutoItX.Run(Path.Combine(Directory.GetCurrentDirectory(), "Executable", APP_TO_TEST), ".");
            AutoItX.WinWaitActive(_mainScreen.WindowTitle, "", _timeout);

            _window = AutoItX.WinGetHandle(_mainScreen.WindowTitle);
        }

        [TestMethod("Current value in the cell is displayed in label properly")]
        public void CurrentValueInTheCellIsDisplayedInLabelProperly()
        {
            IntPtr grid = AutoItX.ControlGetHandle(_window, _mainScreen.GrdDetails);

            AutoItX.ControlFocus(_window, grid);
            AutoItX.ControlClick(_window, grid);
            AutoItX.ControlSend(_window, grid, "{TAB}");

            AutoItX.Sleep(2000);
            AutoItX.Send("test");
            AutoItX.Sleep(2000);

            IntPtr btnGetText = AutoItX.ControlGetHandle(_window, _mainScreen.BtnGetCurrentCellValue);
            AutoItX.ControlClick(_window, btnGetText);

            IntPtr resultLabel = AutoItX.ControlGetHandle(_window, _mainScreen.LblCurrentCellValue);
            var res = AutoItX.ControlGetText(_window, resultLabel);

            Assert.IsTrue(res.Equals("Active cell value: test", StringComparison.CurrentCultureIgnoreCase));
        }

        [TestCleanup]
        public void CleanUp()
        {
            //AutoItX.WinKill(window);
        }
    }
}
