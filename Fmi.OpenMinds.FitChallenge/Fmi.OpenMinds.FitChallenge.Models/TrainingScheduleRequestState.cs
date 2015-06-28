using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Fmi.OpenMinds.FitChallenge.Models
{
    public enum TrainingScheduleRequestState
    {
        Unknown     = 0,
        New         = 1 << 0,
        Approved    = 1 << 1,
        Rejected    = 1 << 2,
        Completed   = 1 << 3
    }

    [NotMapped]
    public class TrainingScheduleRequestStateMachine
    {
        static private Dictionary<TrainingScheduleRequestState, List<TrainingScheduleRequestState>> sTransitionTable; // the key represents the old state, the values list represent the possible new states after a transation

        static TrainingScheduleRequestStateMachine()
        {
            sTransitionTable = new Dictionary<TrainingScheduleRequestState, List<TrainingScheduleRequestState>>();
            sTransitionTable.Add(TrainingScheduleRequestState.New, new List<TrainingScheduleRequestState> { TrainingScheduleRequestState.Approved, TrainingScheduleRequestState.Rejected });
            sTransitionTable.Add(TrainingScheduleRequestState.Approved, new List<TrainingScheduleRequestState> { TrainingScheduleRequestState.Completed });
        }

        public static bool isValidTransition(TrainingScheduleRequestState oldState, TrainingScheduleRequestState newState)
        {
            List<TrainingScheduleRequestState> newStateList;
            return (sTransitionTable.TryGetValue(oldState, out newStateList)) && (newStateList.Contains(newState));
        }

        public static TrainingScheduleRequestState getFirstDirectPrevState(TrainingScheduleRequestState state)
        {
            return sTransitionTable.FirstOrDefault(dictItem => dictItem.Value.Contains(state)).Key;
        }

        public static bool isFinalState(TrainingScheduleRequestState state)
        {
            List<TrainingScheduleRequestState> newStateList;
            return (false == sTransitionTable.TryGetValue(state, out newStateList)) || (null == newStateList) || ((null != newStateList) && (false == newStateList.Any()));
        }
    }
}
