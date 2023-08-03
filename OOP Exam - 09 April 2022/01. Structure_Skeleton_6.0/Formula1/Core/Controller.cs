using Formula1.Core.Contracts;
using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Repositories;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Core
{
    public class Controller : IController
    {
        private PilotRepository pilotRepository;
        private RaceRepository raceRepository;
        private FormulaOneCarRepository carRepository;

        public Controller()
        {
            this.pilotRepository = new PilotRepository();
            this.raceRepository = new RaceRepository();
            this.carRepository = new FormulaOneCarRepository();
        }

        public string CreatePilot(string fullName)
        {
            var checkIfPilotNameExist = pilotRepository.FindByName(fullName);
            if (checkIfPilotNameExist != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));
            }

            IPilot pilot = new Pilot(fullName);
            this.pilotRepository.Add(pilot);

            return string.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            if (carRepository.FindByName(model) != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CarExistErrorMessage, model));
            }

            if (type != nameof(Ferrari) && type != nameof(Williams))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidTypeCar, type));
            }

            IFormulaOneCar car;
            if (type == nameof(Ferrari))
            {
                car = new Ferrari(model, horsepower, engineDisplacement);
            }
            else
            {
                car = new Williams(model, horsepower, engineDisplacement);
            }

            this.carRepository.Add(car);

            return string.Format(OutputMessages.SuccessfullyCreateCar, type, model);
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            var checkIfRaceAlreadyExist = raceRepository.FindByName(raceName);
            if (checkIfRaceAlreadyExist != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExistErrorMessage, raceName));
            }

            IRace race = new Race(raceName, numberOfLaps);
            this.raceRepository.Add(race);

            return string.Format(OutputMessages.SuccessfullyCreateRace, raceName);
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            var currentPilot = this.pilotRepository.FindByName(pilotName);

            if (currentPilot == null || currentPilot.Car != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            }

            //var checkIfPilotHaveACar = currentPilot.Car;
            //if (checkIfPilotHaveACar != null)
            //{
            //    throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            //}

            var checkIfCarExist = carRepository.FindByName(carModel);

            if (checkIfCarExist == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));
            }

            IFormulaOneCar currentCar = this.carRepository.FindByName(carModel);
            currentPilot.AddCar(currentCar);
            this.carRepository.Remove(currentCar);

            var carType = currentCar.GetType().Name;
            return string.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, carType, carModel);
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            var checkIfCurrentRaceExist = raceRepository.FindByName(raceName);
            if (checkIfCurrentRaceExist == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }

            var checkIfPilotExist = this.pilotRepository.FindByName(pilotFullName);

            if (checkIfPilotExist == null || !checkIfPilotExist.CanRace || checkIfCurrentRaceExist.Pilots.Contains(checkIfPilotExist))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }

            //var checkIfPilotCanRace = checkIfPilotExist.CanRace;
            //var checkIfPilotIsAlreadyInTheCurrentRace = checkIfCurrentRaceExist.Pilots.FirstOrDefault(p => p.FullName == pilotFullName);
            //if (checkIfPilotCanRace == false || checkIfPilotIsAlreadyInTheCurrentRace != null)
            //{
            //    throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            //}

            checkIfCurrentRaceExist.AddPilot(checkIfPilotExist);
            return string.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);
        }

        public string StartRace(string raceName)
        {

            var currentRace = raceRepository.FindByName(raceName);
            if (currentRace == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }

            if (currentRace.Pilots.Count < 3)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRaceParticipants, raceName));
            }

            if (currentRace.TookPlace)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));
            }

            int raceLaps = currentRace.NumberOfLaps;
            List<IPilot> orderedPilotsByRaceCalculator = pilotRepository.Models.OrderByDescending(c => c.Car.RaceScoreCalculator(raceLaps)).ToList();
            
            StringBuilder sb = new StringBuilder();
            int count = 0;

            var firstThreePilots = orderedPilotsByRaceCalculator.Take(3);
            foreach (var pilot in firstThreePilots)
            {                
                count++;
                if (count == 1)
                {
                    sb.AppendLine($"Pilot {pilot.FullName} wins the {raceName} race.");
                    pilot.WinRace();
                }
                else if (count == 2)
                {
                    sb.AppendLine($"Pilot {pilot.FullName} is second in the {raceName} race.");
                }
                else if (count == 3)
                {
                    sb.AppendLine($"Pilot {pilot.FullName} is third in the {raceName} race.");
                    break;
                }
            }

            currentRace.TookPlace = true;
            return sb.ToString().TrimEnd();
        }

        public string PilotReport()
        {
            StringBuilder sb = new StringBuilder();

            var pilotsOrderedByWins = this.pilotRepository.Models.OrderByDescending(p => p.NumberOfWins);

            foreach (var pilot in pilotsOrderedByWins)
            {
                sb.AppendLine(pilot.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string RaceReport()
        {
            StringBuilder sb = new StringBuilder();

            var racesThatWasExecuted = raceRepository.Models.Where(r => r.TookPlace == true);
            foreach (var race in racesThatWasExecuted)
            {
                sb.AppendLine(race.RaceInfo());
            }

            return sb.ToString().TrimEnd();
        }   
    }    
}
