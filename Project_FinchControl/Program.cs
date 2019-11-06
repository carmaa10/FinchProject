using System;
using System.Collections.Generic;
using FinchAPI;
using System.IO;
using System.Globalization;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title:            Talent Show
    // Application Type: Console
    // Description:      An application that shows off what 
    //                   abilities the Finch Robot has
    //
    // Author:           Carma Aten
    // Dated Created:    10/1/2019
    // Last Modified:    11/6/2019
    //
    // **************************************************

    // **************************************************
    // IDEAS
    // * Happy/sad tones
    // --------------------------------------------------

    class Program
    {
        private enum Command
        {
            NONE,

            NORMALCOMMANDS, // ---------
            MOVEFORWARD,
            MOVEBACKWARD,
            STOPMOTORS,
            WAIT,
            TURNRIGHT,
            TURNLEFT,
            LEDON,
            LEDOFF,
            DONE,

            SPECIALCOMMANDS, // ---------
            SWIM,
            GETCURRENTTEMP,
            GETCURRENTLIGHTLEVEL,
            PLAYSONG
        }

        static void Main(string[] args)
        {
            DisplayWelcomeScreen();
            //DisplayLoginRegisterOption();
            DisplayMainMenu();
            DisplayClosingScreen();
        }

        static void DisplayMainMenu()
        {
            // -------------
            // Create Finch
            // -------------
            Finch stingRay = new Finch();

            bool finchRobotConnected = DisplayConnectFinchRobot(stingRay);
            bool quitApplication = false;
            string menuChoice;
            string needToConnect = "Please go back to the Main Menu and connect Finch robot (Choice #1).";
            string needToDisconnect = "Please go back to the Main Menu and disconnect Finch robot (Choice #6).";
            string faultyInput = "Please indicate the number corrosponding to your choice.";

            do
            {
                DisplayScreenHeader("Main Menu");

                // ----------------------
                // Get users menu choice
                // ----------------------
                Console.WriteLine("\n1. Connect Finch Robot" +
                    "\n2. Talent Show" +
                    "\n3. Data Recorder" +
                    "\n4. Alarm System" +
                    "\n5. User Programming" +
                    "\n6. Disconnect Finch" +
                    "\n7. Change Theme" +
                    "\n8. Exit");
                menuChoice = Console.ReadLine().Trim();

                // --------------------
                // Process user choice
                // --------------------
                switch (menuChoice)
                {
                    case "1":
                        finchRobotConnected = DisplayConnectFinchRobot(stingRay);
                        break;

                    case "2":
                        if (finchRobotConnected == true)
                        {
                            DisplayTalentShow(stingRay);
                        }
                        else
                        {
                            DisplayErrorMessage(needToConnect);
                        }
                        break;

                    case "3":
                        if (finchRobotConnected)
                        {
                            DisplayDataRecorder(stingRay);
                        }
                        else
                        {
                            DisplayErrorMessage(needToConnect);
                        }
                        break;

                    case "4":
                        if (finchRobotConnected)
                        {
                            DisplayAlarmSystem(stingRay);
                        }
                        else
                        {
                            DisplayErrorMessage(needToConnect);
                        }
                        break;

                    case "5":
                        if (finchRobotConnected)
                        {
                            DisplayUserProgramming(stingRay);
                        }
                        else
                        {
                            DisplayErrorMessage(needToConnect);
                        }
                        break;

                    case "6":
                        finchRobotConnected = DisplayDisconnectFinchRobot(stingRay);
                        break;

                    case "7":
                        DisplayChooseTheme();
                        break;

                    case "8":
                        if (finchRobotConnected == false)
                        {
                            quitApplication = true;
                        }
                        else
                        {
                            DisplayErrorMessage(needToDisconnect);
                        }
                        break;

                    case "q":
                        stingRay.disConnect();
                        quitApplication = true;
                        break;

                    default:
                        DisplayErrorMessage(faultyInput);
                        break;
                }
            } while (!quitApplication);
        }

        #region TALENT SHOW
        // ************************
        // IDEAS
        //
        // ------------------------

        static void DisplayTalentShow(Finch finchRobot)
        {
            string menuChoice;

            DisplayScreenHeader("Talent Show");

            Console.WriteLine("The Finch robot is ready to show you what it\'s got.");
            Console.WriteLine("Would you rather: " +
                "\n1. Choose what Ray does " +
                "\n2. Watch everything ");
            menuChoice = Console.ReadLine().Trim();
            switch (menuChoice)
            {
                case "1":
                    DisplayChooseTalentMenu(finchRobot);
                    break;

                case "2":
                    PerformSong(finchRobot);
                    MovementRoutine(finchRobot);
                    PerformLightShow(finchRobot);
                    break;

                default:
                    DisplayErrorMessage("Didn\'t recognize choice");
                    DisplayContinuePrompt("retry");
                    break;
            }
        }

        static void DisplayChooseTalentMenu(Finch finchRobot)
        {
            string menuChoice;
            bool keepLopping = true;

            do
            {
                DisplayScreenHeader("Choose what the Finch does");
                Console.WriteLine("What would you like Ray to do?" +
                    "\n1. Play a song " +
                    "\n2. Move around " +
                    "\n3. Perform a light show " +
                    "\n4. Exit");
                menuChoice = Console.ReadLine().Trim();
                switch (menuChoice)
                {
                    case "1":
                        PerformSong(finchRobot);
                        break;

                    case "2":
                        ShowMovement(finchRobot);
                        break;

                    case "3":
                        PerformLightShow(finchRobot);
                        break;

                    case "4":
                        keepLopping = false;
                        DisplayContinuePrompt("continue");
                        break;

                    default:
                        DisplayErrorMessage("Didn\'t recognize choice");
                        DisplayTalentShow(finchRobot);
                        break;
                }
            } while (keepLopping);
        }

        #region PERFORM SONG
        static void PerformSong(Finch finchRobot)
        {
            DisplayScreenHeader("Play a song");
            Console.WriteLine("Ray is ready to play his song. ");
            DisplayContinuePrompt("play");

            Console.WriteLine("Ray is playing \"Take On Me\" by A-Ha");

            // FIX FIRST 3 NOTES, they sound HORRIBLE
            //.A // Take
            PlayNote(finchRobot, 220, 1500);

            // G# // On
            PlayNote(finchRobot, 415, 1500);

            // A // Me
            PlayNote(finchRobot, 440, 1500);

            // <E // (take)
            PlayNote(finchRobot, 659, 500);

            // <f# // (on)
            PlayNote(finchRobot, 740, 500);

            // <E // (me)
            PlayNote(finchRobot, 659, 500);

            // A // Take
            PlayNote(finchRobot, 440, 1500);

            // <E // Me
            PlayNote(finchRobot, 659, 1500);

            // <F# // On
            PlayNote(finchRobot, 740, 1500);

            // <E // (take)
            PlayNote(finchRobot, 659, 500);

            // <f# // (on)
            PlayNote(finchRobot, 740, 500);

            // <E // (me)
            PlayNote(finchRobot, 659, 500);

            // <C# // I'll
            PlayNote(finchRobot, 554, 1500);

            // <G# // Be
            PlayNote(finchRobot, 830, 1500);

            // <A // Gone
            PlayNote(finchRobot, 880, 1500);

            // <B // In
            PlayNote(finchRobot, 998, 300);

            // *C# // A
            PlayNote(finchRobot, 1109, 300);

            // <B // Day
            PlayNote(finchRobot, 998, 400);

            // <A // Or
            PlayNote(finchRobot, 880, 700);

            // *E // Two
            PlayNote(finchRobot, 1318, 2500);

            DisplayContinuePrompt("continue");
        }

        static void PlayNote(Finch finchRobot, int frequency, int length)
        {
            finchRobot.noteOn(frequency);
            PauseFinch(finchRobot, length);
            finchRobot.noteOff();
        }

        #endregion // PERFORM SONG

        #region SHOW MOVEMENT
        static void ShowMovement(Finch finchRobot)
        {
            DisplayScreenHeader("Finch Movement");

            DisplayMovementMenu(finchRobot);

            DisplayContinuePrompt("continue");
        }

        static void DisplayMovementMenu(Finch finchRobot)
        {
            bool keepLooping = true;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Make the Finch move!");
                Console.WriteLine("What should Ray do?" +
                    "\n1. Move Forward" +
                    "\n2. Move Backward" +
                    "\n3. Turn left" +
                    "\n4. Turn right" +
                    "\n5. Exit Movement");
                menuChoice = Console.ReadLine().Trim();
                switch (menuChoice)
                {
                    case "1":
                        FinchMove(finchRobot, 100, 100, 1000);
                        break;

                    case "2":
                        FinchMove(finchRobot, -100, -100, 1000);
                        break;

                    case "3":
                        FinchMove(finchRobot, 100, 200, 1000);
                        break;

                    case "4":
                        FinchMove(finchRobot, 200, 100, 1000);
                        break;

                    case "5":
                        keepLooping = false;
                        break;

                    default:
                        DisplayErrorMessage("Please respond with the number corresponding with your choice. ");
                        break;
                }
            } while (keepLooping);
        }

        static void FinchMove(Finch finchRobot, int speed1, int speed2, int length)
        {
            finchRobot.setMotors(speed1, speed2);
            PauseFinch(finchRobot, length);
            finchRobot.setMotors(0, 0);
        }

        static void FinchSwim(Finch finchRobot)
        {
            for (int i = 0; i < 4; i++)
            {
                FinchMove(finchRobot, 200, 100, 500);
                FinchMove(finchRobot, 100, 200, 500);
            }

            DisplayContinuePrompt("continue");
        }

        static void MovementRoutine(Finch finchRobot)
        {
            FinchSwim(finchRobot);
            FinchMove(finchRobot, -200, -200, 1000);
            DisplayContinuePrompt("continue");
            FinchMove(finchRobot, 200, 200, 1000);
            DisplayContinuePrompt("continue");
        }

        #endregion // MOVEMENT

        #region LIGHT SHOW
        /// <summary>
        /// Displays morse code in flashes for "ray".
        /// </summary>
        static void PerformLightShow(Finch finchRobot)
        {
            int lightLengthOneDash = 1000;
            int lightLengthTwoDash = lightLengthOneDash * 2;
            int lightLengthDot = 500;
            int pauseLength = 500;
            int pauseLengthBetweenLetters = pauseLength * 2;
            int pauseLengthBetweenWords = pauseLength * 4;

            DisplayScreenHeader("Finch Light Show");

            Console.WriteLine("Ray is ready to do the light thing.");
            DisplayContinuePrompt("start");

            Console.WriteLine("Ray is flashing his name in morse code!");

            // r .-.
            MorseDot(finchRobot, lightLengthDot, 255, 0, 0);
            PauseFinch(finchRobot, pauseLength);
            MorseDash(finchRobot, lightLengthOneDash, 255, 0, 0);
            PauseFinch(finchRobot, pauseLength);
            MorseDot(finchRobot, lightLengthDot, 255, 0, 0);

            PauseFinch(finchRobot, pauseLengthBetweenLetters);

            // a .-
            MorseDot(finchRobot, lightLengthDot, 0, 255, 0);
            PauseFinch(finchRobot, pauseLength);
            MorseDash(finchRobot, lightLengthOneDash, 0, 255, 0);

            PauseFinch(finchRobot, pauseLengthBetweenLetters);

            // y -.--
            MorseDash(finchRobot, lightLengthOneDash, 0, 0, 255);
            PauseFinch(finchRobot, pauseLength);
            MorseDot(finchRobot, lightLengthDot, 0, 0, 255);
            PauseFinch(finchRobot, pauseLength);
            MorseDash(finchRobot, lightLengthTwoDash, 0, 0, 255);

            DisplayContinuePrompt("continue");
        }

        static void MorseDot(Finch finchRobot, int length, int r, int g, int b)
        {
            finchRobot.setLED(r, g, b);
            PauseFinch(finchRobot, length);
            finchRobot.setLED(0, 0, 0);
        }

        static void MorseDash(Finch finchRobot, int length, int r, int g, int b)
        {
            finchRobot.setLED(r, g, b);
            PauseFinch(finchRobot, length);
            finchRobot.setLED(0, 0, 0);
        }
        #endregion // LIGHT SHOW

        #endregion // TALENT SHOW

        #region DATA RECORDER
        // ************************
        // IDEAS
        //
        // ------------------------

        static void DisplayDataRecorder(Finch finchRobot)
        {
            double dataPointFrequency;
            int numberDataPoints;

            DisplayScreenHeader("Data Recorder");

            DisplayDataRecorderInstructions();
            dataPointFrequency = DisplayGetDataFrequency();
            numberDataPoints = DisplayGetDataNumber();

            double[] data = new double[numberDataPoints];

            Console.Write("What type of data would you like to get?" +
                "\n1. Light Data" +
                "\n2. Temperature Data\n");
            switch (DisplayGetUserResponse())
            {
                case "1":
                    DisplayGetLightData(numberDataPoints, dataPointFrequency, data, finchRobot);
                    break;
                case "2":
                    DisplayGetTemperatureData(numberDataPoints, dataPointFrequency, data, finchRobot);
                    break;
                default:
                    break;
            }

            DisplayData(data);

            DisplayContinuePrompt("return to the main menu");
        }

        /// <summary>
        /// Display Data Recorder Intructions
        /// </summary>
        static void DisplayDataRecorderInstructions()
        {
            Console.Clear();
            DisplayScreenHeader("Data Recorder Instructions");
            Console.WriteLine("\n1. Enter how often (in seconds) you would like the Finch to record a data point"
                + "\n2. Enter how many data points you would like recorded"
                + "\n3. Wait for results!"
                + "\n\tTemperature data will be displayed in fahrenheit\n");
        }

        /// <summary>
        /// Get data frequency, parse response
        /// </summary>
        static double DisplayGetDataFrequency()
        {
            double dataFrequency;

            Console.Write("Enter frequency of recordings: ");
            double.TryParse(Console.ReadLine(), out dataFrequency);

            return dataFrequency;
        }

        /// <summary>
        /// get number of data points, parse response
        /// </summary>
        static int DisplayGetDataNumber()
        {
            int numberDataPoints;

            Console.Write("Enter number of data points: ");
            int.TryParse(Console.ReadLine(), out numberDataPoints);

            return numberDataPoints;
        }

        /// <summary>
        /// gathers the temperature data to user's preferences
        /// </summary>
        static void DisplayGetTemperatureData(
            int numberDataPoints,
            double dataPointFrequency,
            double[] temperatures,
            Finch finchRobot)
        {
            DisplayScreenHeader("Record Temperature Data");

            for (int t = 0; t < numberDataPoints; t++)
            {
                temperatures[t] = finchRobot.getTemperature();
                GetCelsiusToFahrenheit(temperatures[t]);
                int milliseconds = (int)(dataPointFrequency * 1000);
                PauseFinch(finchRobot, milliseconds);

                DisplayDataPointRecorded(finchRobot, dataPointFrequency);
            }
        }

        /// <summary>
        /// gathers the light data to user's preferences
        /// </summary>
        static void DisplayGetLightData(
            int numberDataPoints,
            double dataPointFrequency,
            double[] brightness,
            Finch finchRobot)
        {
            double dataPoint;
            DisplayScreenHeader("Record Light Data");

            for (int ld = 0; ld < numberDataPoints; ld++)
            {
                int numberLightData = 2;
                int[] lightDataLeft = new int[numberDataPoints];
                lightDataLeft[ld] = finchRobot.getLeftLightSensor();

                int[] lightDataRight = new int[numberDataPoints];
                lightDataRight[ld] = finchRobot.getRightLightSensor();

                dataPoint = (lightDataLeft[ld] + lightDataRight[ld]) / numberLightData;
                brightness[ld] = dataPoint;

                int milliseconds = (int)(dataPointFrequency * 1000);
                PauseFinch(finchRobot, milliseconds);

                DisplayDataPointRecorded(finchRobot, dataPointFrequency);
            }
        }

        /// <summary>
        /// uses a loop to display the data recorded
        /// </summary>
        static void DisplayData(double[] data)
        {
            DisplayScreenHeader("Your Data");
            for (int dataPoint = 0; dataPoint < data.Length; dataPoint++)
            {
                Console.WriteLine($"Data Point {dataPoint + 1}: {data[dataPoint]}");
            }
        }

        /// <summary>
        /// converts default celcius temperature value to fahrenheit
        /// </summary>
        static double GetCelsiusToFahrenheit(double dataPoint)
        {
            double convertedDataPoint;
            double tempOffset = 32;
            double conversionValue = (9 / 5);

            convertedDataPoint = (dataPoint * conversionValue) + tempOffset;

            return convertedDataPoint;
        }

        /// <summary>
        /// makes finch robot blink light when a data point is recorded
        /// </summary>
        static void DisplayDataPointRecorded(Finch finchRobot, double dataPointFrequency)
        {
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait((int)(dataPointFrequency) / 2);
            finchRobot.setLED(0, 0, 0);
        }

        #endregion // DATA RECORDER

        #region ALARM SYSTEM
        // ************************
        // IDEAS
        //
        // ------------------------

        static void DisplayAlarmSystem(Finch finchRobot)
        {
            string alarmType;
            int maxSeconds;
            double upperThreshold;
            double lowerThreshold;
            bool thresholdExceeded;

            DisplayScreenHeader("Alarm System");

            alarmType = DisplayGetAlarmType();
            maxSeconds = DisplayGetMaxSeconds();
            upperThreshold = DisplayGetUpperThreshold(finchRobot, alarmType);
            lowerThreshold = DisplayGetLowerThreshold(finchRobot, alarmType);

            thresholdExceeded = MonitorCurrentLevels(finchRobot, upperThreshold, lowerThreshold, maxSeconds, alarmType);
            switch (thresholdExceeded)
            {
                case true:
                    Console.WriteLine("Maximum reached!");
                    DisplaySoundAlarm(finchRobot);
                    DisplayContinuePrompt("exit to main menu");
                    break;
                case false:
                    Console.WriteLine("Threshold not reached");
                    DisplayContinuePrompt("exit to main menu");
                    break;
                default:
                    break;
            }
        }

        static void DisplaySoundAlarm(Finch finchRobot)
        {
            for (int repeat = 0; repeat < 3; repeat++)
            {
                PlayNote(finchRobot, 1500, 500);
                PlayNote(finchRobot, 500, 500);
                DisplayLED(finchRobot, 255, 0, 0, 500);
                DisplayLED(finchRobot, 255, 0, 0, 500);
            }
        }

        static bool MonitorCurrentLevels(Finch finchRobot, double upperThreshold, double lowerThreshold, int maxSeconds, string alarmType)
        {
            bool thresholdExceeded = false;
            double seconds = 0;
            double currentLightLevel;
            double currentTemperature;

            switch (alarmType)
            {
                case "1": // light
                    while (!thresholdExceeded && seconds <= maxSeconds)
                    {
                        currentLightLevel = finchRobot.getLeftLightSensor();

                        DisplayScreenHeader("Monitor Light Levels");

                        Console.WriteLine($"Maximum light level: {upperThreshold}");
                        Console.WriteLine($"Current light level: {currentLightLevel}");
                        Console.WriteLine($"Maximum light level: {lowerThreshold}");

                        if (currentLightLevel > upperThreshold || currentLightLevel < lowerThreshold)
                        {
                            thresholdExceeded = true;
                            break;
                        }
                        else
                        {
                            thresholdExceeded = false;
                        }

                        PauseFinch(finchRobot, 500);
                        seconds += 0.5;
                    }

                    break;

                case "2": // temperature
                    while (!thresholdExceeded && seconds <= maxSeconds)
                    {
                        currentTemperature = finchRobot.getTemperature();

                        DisplayScreenHeader("Monitor Temperature");

                        Console.WriteLine($"Maximum temperature: {upperThreshold}");
                        Console.WriteLine($"Current temperature: {currentTemperature}");
                        Console.WriteLine($"Maximum temperature: {lowerThreshold}");

                        if (currentTemperature > upperThreshold || currentTemperature < lowerThreshold)
                        {
                            thresholdExceeded = true;
                            break;
                        }
                        else
                        {
                            thresholdExceeded = false;
                        }

                        PauseFinch(finchRobot, 500);
                        seconds += 0.5;
                    }

                    break;
                default:
                    break;
            }

            return thresholdExceeded;
        }

        static string DisplayGetAlarmType()
        {
            string alarmType = "";
            bool keepLooping = true;

            while (keepLooping)
            {
                Console.WriteLine("What type of alarm would you like to set?"
                + "\n1. Light"
                + "\n2. Temperature");
                alarmType = Console.ReadLine().Trim();
                switch (alarmType)
                {
                    case "1":
                    case "2":
                        keepLooping = false;
                        break;
                    default:
                        DisplayErrorMessage("Please input a valid menu option (1 or 2)");
                        keepLooping = true;
                        break;
                }
            }

            return alarmType;
        }

        static int DisplayGetMaxSeconds()
        {
            int maxSeconds = 0;
            bool keepLooping = true;
            bool didParse = false;

            while (keepLooping)
            {
                Console.WriteLine("max seconds: ");
                didParse = int.TryParse(Console.ReadLine(), out maxSeconds);
                if (didParse && maxSeconds != 0)
                {
                    keepLooping = false;
                }
                else
                {
                    DisplayErrorMessage("Please enter an integer (3, 10)");
                }
            }

            return maxSeconds;
        }

        static double DisplayGetUpperThreshold(Finch finchRobot, string alarmType)
        {
            double threshold = 0;
            bool parseWorked = false;
            bool keepLooping = true;

            DisplayScreenHeader("Upper Threshold Value");

            while (keepLooping)
            {
                switch (alarmType)
                {
                    case "1": // light
                        Console.Write($"Current light level: {finchRobot.getLeftLightSensor()}");
                        Console.WriteLine("\nEnter max light level (0 - 255): ");
                        break;
                    case "2": // temperature
                        Console.Write($"Current temperature: {finchRobot.getTemperature()}");
                        Console.WriteLine("\nEnter max temperature (celcius): ");
                        break;
                    default:
                        break;
                }

                parseWorked = double.TryParse(Console.ReadLine(), out threshold);
                if (parseWorked)
                {
                    keepLooping = false;
                }
                else
                {
                    DisplayErrorMessage("Please enter e number value (3.5, 10)");
                    keepLooping = true;
                }
            }



            DisplayContinuePrompt("continue");

            return threshold;
        }

        static double DisplayGetLowerThreshold(Finch finchRobot, string alarmType)
        {
            double threshold = 0;
            bool parseWorked = false;
            bool keepLooping = true;

            DisplayScreenHeader("Lower Threshold Value");

            while (keepLooping)
            {
                switch (alarmType)
                {
                    case "1": // light
                        Console.Write($"Current light level: {finchRobot.getLeftLightSensor()}");
                        Console.WriteLine("\nEnter min light level (0 - 255): ");
                        break;
                    case "2": // temperature
                        Console.Write($"Current temperature: {finchRobot.getTemperature()}");
                        Console.WriteLine("\nEnter min temperature (celcius): ");
                        break;
                    default:
                        break;
                }

                parseWorked = double.TryParse(Console.ReadLine(), out threshold);
                if (parseWorked)
                {
                    keepLooping = false;
                }
                else
                {
                    DisplayErrorMessage("Please enter e number value (3.5, 10)");
                    keepLooping = true;
                }
            }



            DisplayContinuePrompt("continue");

            return threshold;
        }

        #endregion // ALARM SYSTEM

        #region USER PROGRAMMING
        // ************************
        // IDEAS
        // * Have it so user picks duration, rbg, speed etc
        // * use tuples to make language better, durations of commands
        // ------------------------

        static void DisplayUserProgramming(Finch finchRobot)
        {
            // normal
            string menuChoice;
            bool quitApplication = false;

            // command parameters tuple
            (int motorSpeed, int ledBrightness, int waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            List<Command> commands = new List<Command>();

            do
            {
                DisplayScreenHeader("Main Menu");

                Console.WriteLine("\n1. Set command parameters" +
                                  "\n2. View possible commands" +
                                  "\n3. Add commands" +
                                  "\n4. View Commands" +
                                  "\n5. Execute Command" +
                                  "\n6. Clear commands" +
                                  //"\n7. Save Data" +
                                  //"\n8. Load Data" +
                                  "\n7. Exit");
                menuChoice = Console.ReadLine().Trim();

                switch (menuChoice)
                {
                    case "1": //
                        commandParameters = DisplayGetCommandParameters();
                        break;

                    case "2":
                        DisplayPossibleFinchCommands();
                        break;

                    case "3": //
                        DisplayGetFinchCommands(finchRobot, commands);
                        break;

                    case "4": //
                        DisplayFinchCommands(commands);
                        break;

                    case "5": //
                        DisplayExecuteFinchCommands(finchRobot, commands, commandParameters);
                        break;

                    case "6": //
                        commands.Clear();
                        break;

                    //case "7": // Save Data
                    //    DisplayWriteUserProgramData(commands);
                    //    break;

                    //case "8": // Load Data
                    //    DisplayReadUserProgram();
                    //    break;

                    case "7":
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine("faulty input");
                        break;
                }
            } while (!quitApplication);
        }

        static void DisplayPossibleFinchCommands()
        {
            bool keepLooping = true;

            DisplayScreenHeader("Possible Commands");
            Console.WriteLine("1. Normal Commands" +
                              "\n2. Special Commands");

            do
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        DisplayPossibleNormalCommands();
                        keepLooping = false;
                        break;
                    case "2":
                        DisplayPossibleSpecialCommands();
                        keepLooping = false;
                        break;
                    default:
                        DisplayErrorMessage("Please enter the number next to your choice (1,2)");
                        break;
                }

            } while (keepLooping);    
        }

        static (int motorSpeed, int ledBrightness, int waitSeconds) DisplayGetCommandParameters()
        {
            (int motorSpeed, int ledBrightness, int waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            bool couldParse = false;

            do
            {
                Console.Write("Enter motor speed (1-255): ");
                couldParse = int.TryParse(Console.ReadLine(), out commandParameters.motorSpeed);

                if (!couldParse)
                {
                    DisplayErrorMessage("Please input a value between 0 and 255");
                }

            } while (!couldParse);

            do
            {
                Console.Write("Enter LED brightness (1-255): ");
                couldParse = int.TryParse(Console.ReadLine(), out commandParameters.ledBrightness);

                if (!couldParse)
                {
                    DisplayErrorMessage("Please input a value between 0 and 255");
                }

            } while (!couldParse);

            do
            {
                Console.Write("Enter wait in seconds: ");
                couldParse = int.TryParse(Console.ReadLine(), out commandParameters.waitSeconds);

                if (!couldParse)
                {
                    DisplayErrorMessage("Please input a value between 0 and 255");
                }

            } while (!couldParse);

            Console.WriteLine($"\tYour Parameters: " +
                $"\nMotor Speed: {commandParameters.motorSpeed}" +
                $"\nLED Brightness: {commandParameters.ledBrightness}" +
                $"\nWait Time: {commandParameters.waitSeconds} seconds");

            DisplayContinuePrompt("continue");

            return commandParameters;
        }

        static void DisplayFinchCommands(List<Command> commands)
        {
            DisplayScreenHeader("Your finch commands");
            foreach (Command command in commands)
            {
                Console.WriteLine(command);
            }

            DisplayContinuePrompt("exit");
        }

        static void DisplayPossibleNormalCommands()
        {
            DisplayScreenHeader("Normal Commands");

            Console.WriteLine(
                "MOVEFORWARD, moves the finch forward\n" +
                "MOVEBACKWARD, moves the finchbackward\n" +
                "STOPMOTORS, stops the finch from moving\n" +
                "WAIT, finch stays doing what it\'s doing for the wait period\n" +
                "TURNRIGHT, finch turns in place to the right\n" +
                "TURNLEFT, finch turns in place to the left\n" +
                "LEDON, turns the finch LED on\n" +
                "LEDOFF, turns finch LED off\n" +
                "DONE, indicates that you are done entering commands \n");

            DisplayContinuePrompt("exit");
        }

        static void DisplayPossibleSpecialCommands()
        {
            DisplayScreenHeader("Special Commands");
            
            Console.WriteLine(
                "SWIM, the finch moves so that it looks like it's swimming, does this for the wait period set \n" +
                "GETCURRENTTEMP, gets the current temperature \n" +
                "GETCURRENTLIGHTLEVEL, gets the current light levels \n" +
                "PLAYSONG, plays the song from the talent show portion \n" +
                " ");

            DisplayContinuePrompt("exit");
        }

        static void DisplayGetFinchCommands(Finch finchRobot, List<Command> commands)
        {
            Command command = Command.NONE;
            string userCommand;

            DisplayScreenHeader("Finch Robot Commands");

            DisplayPossibleNormalCommands();

            while (command != Command.DONE)
            {
                Console.Write("Command: ");
                userCommand = Console.ReadLine().ToUpper().Trim();
                Enum.TryParse(userCommand, out command);

                if (command != Command.NONE)
                {
                    commands.Add(command);
                }
                else if (command == Command.SPECIALCOMMANDS)
                {
                    DisplayPossibleSpecialCommands();
                }
                else if (command == Command.NORMALCOMMANDS)
                {
                    DisplayPossibleNormalCommands();
                }
                else
                {
                    Console.WriteLine("WARNING: Invalid command, command not added");
                }
            }

            DisplayContinuePrompt("exit");
        }

        static void DisplayExecuteFinchCommands(
            Finch finchRobot,
            List<Command> commands,
            (int motorSpeed, int ledBrightness, int waitSeconds) commandParameters)
        {
            int motorSpeed = commandParameters.motorSpeed;
            int ledBrightness = commandParameters.ledBrightness;
            int waitMilliSeconds = commandParameters.waitSeconds * 1000;

            DisplayScreenHeader("Executing Finch Commands");

            DisplayContinuePrompt("start executing commands");

            foreach (Command command in commands)
            {
                switch (command)
                {
                    case Command.NONE:
                        break;
                    case Command.NORMALCOMMANDS:
                        DisplayPossibleNormalCommands();
                        break;
                    case Command.SPECIALCOMMANDS:
                        DisplayPossibleSpecialCommands();
                        break;
                    case Command.MOVEFORWARD:
                        Console.WriteLine("Moving Forward");
                        finchRobot.setMotors(motorSpeed, motorSpeed);
                        break;
                    case Command.MOVEBACKWARD:
                        Console.WriteLine("Moving Backward");
                        finchRobot.setMotors(-motorSpeed, -motorSpeed);
                        break;
                    case Command.STOPMOTORS:
                        Console.WriteLine("Stopping");
                        finchRobot.setMotors(0, 0);
                        break;
                    case Command.WAIT:
                        Console.WriteLine("Waiting");
                        finchRobot.wait(waitMilliSeconds);
                        break;
                    case Command.TURNRIGHT:
                        Console.WriteLine("Turning Right");
                        finchRobot.setMotors(motorSpeed, -motorSpeed);
                        break;
                    case Command.TURNLEFT:
                        Console.WriteLine("Turning Left");
                        finchRobot.setMotors(-motorSpeed, motorSpeed);
                        break;
                    case Command.LEDON:
                        Console.WriteLine("Setting LED");
                        finchRobot.setLED(ledBrightness, ledBrightness, ledBrightness);
                        break;
                    case Command.LEDOFF:
                        Console.WriteLine("Turning Off LED");
                        finchRobot.setLED(0, 0, 0);
                        break;

                    case Command.SWIM:
                        FinchSwim(finchRobot);
                        break;
                    case Command.GETCURRENTTEMP:
                        Console.WriteLine($"Current Temperature: {finchRobot.getTemperature()}");
                        break;
                    case Command.GETCURRENTLIGHTLEVEL:
                        Console.WriteLine($"Current Light Level: {finchRobot.getLeftLightSensor()}");
                        break;
                    case Command.PLAYSONG:
                        PerformSong(finchRobot);
                        break;

                    case Command.DONE:
                        finchRobot.setMotors(0, 0);
                        finchRobot.setLED(0, 0, 0);
                        break;
                }
            }

            DisplayContinuePrompt("exit");
        }

        static void DisplayWriteUserProgramData(List<Command> commands)
        {
            string dataPath = @"Data\Data.txt";
            List<string> commandsToString = new List<string>();

            DisplayScreenHeader("Save commands to a file");

            Console.WriteLine("Ready to save the commands to the Data file");
            DisplayContinuePrompt("save");

            // CREATE list of command strings
            foreach (Command command in commands)
            {
                commandsToString.Add(command.ToString());
            }

            File.WriteAllLines(dataPath, commandsToString.ToArray());

            DisplayContinuePrompt("Continue");
        }

        static List<Command> DisplayReadUserProgram()
        {
            string dataPath = @"Data\Data.txt";
            List<Command> commands = new List<Command>();
            string[] commandsAsStrings;

            DisplayScreenHeader("Load commands from file");

            Console.WriteLine("Ready to load the commands from the file");
            DisplayContinuePrompt("continue");

            commandsAsStrings = File.ReadAllLines(dataPath);

            // put array into Command list
            Command command;
            foreach (string stringCommand in commandsAsStrings)
            {
                Enum.TryParse(stringCommand, out command);

                commands.Add(command);
            }

            Console.WriteLine("\nCommands loaded successfully");

            DisplayContinuePrompt("exit");

            return commands;
        }

        #endregion // USER PROGRAMMING

        #region CONNECT/DISCONNECT
        // ************************
        // IDEAS
        //
        // ------------------------

        static bool DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            bool finchRobotConnected;
            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("Ready to disconnect Finch robot.");
            DisplayContinuePrompt("disconnect");
            finchRobot.disConnect();
            finchRobotConnected = false;

            Console.WriteLine("\nFinch robot is now disconnected.");

            DisplayContinuePrompt("continue");

            return finchRobotConnected;
        }

        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            bool finchRobotConnected;
            int numberAttempts = 0;

            DisplayScreenHeader("Connect Finch Robot");

            do
            {
                Console.WriteLine("Are you ready to connect to the finch robot? USB must be plugged in. ");
                DisplayContinuePrompt("connect");

                finchRobotConnected = finchRobot.connect();
                if (finchRobotConnected)
                {
                    finchRobot.setLED(0, 255, 0);

                    Console.WriteLine("\nFinch robot is now connected.");
                    numberAttempts = 4;
                }
                else
                {
                    DisplayErrorMessage("Unable to connect to Finch robot.");
                    numberAttempts += 1;
                }
            } while (numberAttempts < 3);

            switch (finchRobotConnected)
            {
                case true:
                    DisplayContinuePrompt("continue");
                    break;
                case false:
                    DisplayErrorMessage("You were unable to connect to the Finch.\nPlease make sure that it is plugged in correctly and your computer recognizes it.");
                    break;
            }



            return finchRobotConnected;
        }

        #endregion // CONNECT/DISCONNECT

        #region LOGIN/REGISTRATION

        // ************************
        // IDEAS
        //
        // ------------------------

        static void DisplayLoginRegisterOption()
        {
            string userResponse;
            bool keepLooping = true;
            string userDatabase = @"Data\Users.txt";

            while (keepLooping)
            {
                DisplayScreenHeader("Login or Register");

                Console.WriteLine(
                    "1. Login" +
                    "\n2. Register" +
                    "\n3. Quit");

                userResponse = Console.ReadLine();

                switch (userResponse)
                {
                    case "1":
                        DisplayLogin(userDatabase);
                        keepLooping = false;
                        break;
                    case "2":
                        DisplayRegister(userDatabase);
                        keepLooping = false;
                        break;
                    case "3":
                        keepLooping = false;
                        break;
                    default:
                        DisplayErrorMessage("Please enter a menu choice (1-3)");
                        keepLooping = true;
                        break;
                }
            }

            DisplayContinuePrompt("continue");
        }

        static void DisplayLogin(string userDatabase)
        {
            string path = @"Data\Users.txt";
            string response;

            string[] allUsers = File.ReadAllLines(path);

            string userName;
            string password;

            DisplayScreenHeader("Log In");

            Console.Write("Username: ");
            userName = Console.ReadLine().Trim();

            foreach (string user in allUsers)
            {
                string[] userInfo = user.Split('|');
                string existingUser = userInfo[0];
                string userPassword = userInfo[1];

                int attempts = 0;

                if (existingUser == userName)
                {
                    for (int attempt = 0; attempt < 3; attempt++)
                    {
                        Console.Write("Password: ");
                        password = Console.ReadLine().Trim();

                        if (userPassword == password)
                        {
                            Console.WriteLine("Login successful");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid password");
                            attempts++;
                        }
                    }

                    if (attempts == 3)
                    {
                        Console.WriteLine("You have reached the maximum number of attempts you are allowed, maybe you forgot you password, or entered the wrong username");
                        DisplayContinuePrompt("continue");
                        DisplayLoginRegisterOption();
                    }

                }
                else if (existingUser != userName)
                {
                    Console.WriteLine("That username does not exist");
                    Console.Write("Would you like to register? (y/n): ");
                    response = Console.ReadLine();
                    if (response == "y")
                    {
                        DisplayRegister(userDatabase);
                    }
                    else
                    {
                        DisplayLoginRegisterOption();
                    }
                    
                }
            }
        }

        static void DisplayRegister(string userDatabase)
        {
            string existingUsers = File.ReadAllText(userDatabase);
            (string userName, string password) userLogin;

            Console.Write("Username: ");
            userLogin.userName = Console.ReadLine().Trim();
            Console.Write("Password: ");
            userLogin.password = Console.ReadLine().Trim();

            File.AppendAllText(userDatabase, $"\n{userLogin.userName}|{userLogin.password}");
        }

        #endregion

        #region THEME 
        static void DisplayChooseTheme()
        {
            DisplayScreenHeader("Choose Theme");

            Console.WriteLine("\n1. Cycle through existing themes " +
                              "\n2. Add a theme");
            switch (Console.ReadLine())
            {
                case "1":
                    DisplayChooseThemeCycle();
                    break;

                case "2":
                    DisplayAddTheme();
                    break;

                default:
                    DisplayErrorMessage("Please enter a valid menu choice");
                    break;
            }
        }

        static void DisplayAddTheme()
        {
            string path = @"Data\Theme.txt";
            string backgroundColor;
            string foregroundColor;
            bool couldParse;
            bool keepLooping = true;
            ConsoleColor temp;

            // **************************************
            // Taken from c# microsoft documentation
            // Creates a TextInfo based on the "en-US" culture.
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

            DisplayScreenHeader("Add a Theme");

            do
            {
                Console.Write("Background Color: ");
                backgroundColor = myTI.ToTitleCase(Console.ReadLine().Trim());
                couldParse = Enum.TryParse(backgroundColor, out temp);
                if (couldParse == false)
                {
                    DisplayErrorMessage("could not understand");
                }
                else
                {
                    keepLooping = false;
                }

            } while (keepLooping);

            // resetting keepLooping
            keepLooping = true;

            do
            {
                Console.Write("Text Color: ");
                foregroundColor = myTI.ToTitleCase(Console.ReadLine().Trim());
                couldParse = Enum.TryParse(foregroundColor, out temp);
                if (couldParse == false)
                {
                    DisplayErrorMessage("could not understand");
                }
                else
                {
                    keepLooping = false;
                }

            } while (keepLooping);



            string themeText = $"\n{backgroundColor}|{foregroundColor}";
            File.AppendAllText(path, themeText);
            Console.WriteLine("Theme Successfully Added");

            DisplayContinuePrompt("continue");
        }

        static void DisplayChooseThemeCycle()
        {
            ConsoleColor background;
            ConsoleColor foreground;

            string[] themes = File.ReadAllLines("Data\\Theme.txt");

            bool keepLooping = true;
            bool validResponse = false;
            string userResponse;

            while (keepLooping)
            {
                foreach (string theme in themes)
                {
                    string[] themeArray = theme.Split('|');

                    string backgroundString = themeArray[0];
                    string foregroundString = themeArray[1];

                    Enum.TryParse(backgroundString, out background);
                    Enum.TryParse(foregroundString, out foreground);

                    Console.Clear();

                    Console.BackgroundColor = background;
                    Console.ForegroundColor = foreground;

                    DisplayScreenHeader("Theme Preview");

                    Console.WriteLine("Do you like this theme? ");
                    Console.WriteLine("\n1. Yes" +
                                      "\n2. No");

                    do
                    {
                        validResponse = false;

                        userResponse = Console.ReadLine();

                        if (userResponse == "1")
                        {
                            keepLooping = false;
                            validResponse = true;
                            break;
                        }
                        else if (userResponse == "2")
                        {
                            keepLooping = true;
                            validResponse = true;
                        }
                        else
                        {
                            DisplayErrorMessage("Please enter the number next to your choice (1, 2)");
                            keepLooping = true;
                        }
                    } while (!validResponse && keepLooping);

                    if (keepLooping == false)
                    {
                        break;
                    }
                }
            }
        }

        #endregion

        #region SCREENS
        /// <summary>
        /// display welcome screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt("start");
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt("exit");
        }

        #endregion // SCREENS

        #region MATH

        static int AverageArrayValues(int[] array)
        {
            int average;
            int sum = 0;

            for (int value = 0; value < array.Length; value++)
            {
                sum += array[value];
            }

            average = sum / array.Length;

            return average;
        }

        #endregion // MATH

        #region MISC
        static void DisplayErrorMessage(string error)
        {
            // 6 represents extra characters from formtting box around message
            RepeatCharacter(error.Length + 6, "*");
            Console.WriteLine($"\n** {error} **");
            RepeatCharacter(error.Length + 6, "*");

            DisplayContinuePrompt("retry");
        }

        static void RepeatCharacter(int numberTimes, string character)
        {
            for (int repeat = 0; repeat < numberTimes; repeat++)
            {
                Console.Write(character);
            }
        }

        static void PauseFinch(Finch finchRobot, int length)
        {
            finchRobot.wait(length);
        }

        static string DisplayGetUserResponse()
        {
            string userResponse;

            userResponse = Console.ReadLine().Trim();

            return userResponse;
        }

        static bool CheckParseWorked(bool parse)
        {
            if (parse)
            {
                parse = true;
            }
            else
            {
                DisplayErrorMessage("Please input a number (10, 2, etc.)");
                parse = false;
            }
            return parse;
        }

        static void DisplayLED(Finch finchRobot, int r, int g, int b, int duration)
        {
            finchRobot.setLED(r, g, b);
            finchRobot.wait(duration);
            finchRobot.setLED(0, 0, 0);
        }

        #endregion // MISC

        #region HELPER METHODS

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt(string action)
        {
            Console.WriteLine();
            Console.WriteLine($"Press any key to {action}.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion // HELPER METHODS
    }
}
