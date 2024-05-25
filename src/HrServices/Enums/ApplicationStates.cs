using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrServices.Enums
{
    // TODO: definitely make this a table
    public enum ApplicationStates
    {
        Hired = 0,
        OfferAccepted = 1,
        OfferRejected = 2,
        OfferSent = 3,
        OfferCancelled = 4,
        FinalInterviewSuccessful = 5,
        FinalInterviewRejected = 6,
        FinalInterviewDone = 7,
        FinalInterviewCancelled = 8,
        FinalInterviewScheduled = 9,
        FinalInterviewPending = 10,
        UnderInterview = 11,
        InterviewScheduled = 12,
        InterviewPending = 13,
        InterviewRejected = 14,
        InterviewCancelled = 15,
        InitialInterviewDone = 16,
        InitialInterviewCancelled = 17,
        InitialInterviewPending = 18,
        InitialInterviewRejected = 19,
        InitialInterviewSuccessful = 20,
        Applied = 21,
        Rejected = 22,
        Canceled = 23,
        CandidateRejected = 24
    }
}
