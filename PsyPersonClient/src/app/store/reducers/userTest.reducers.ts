import { EUserTestActions, UserTestActions } from "../actions/userTest.actions";
import { initialUserTestState, UserTestState } from "../state/userTest.state";

export const userTestReducers = (
    state = initialUserTestState,
    action: UserTestActions
): UserTestState => {
    switch(action.type){
        case EUserTestActions.GetUserTestUsersSuccess: {
            return {
                ...state,
                userTestUsers:{
                    data : action.payload.data,
                    total : action.payload.total,
                    loading : action.payload.loading
                }
            };
        }
        case EUserTestActions.GetUserTestsSuccess: {
            return {
                ...state,
                userTests:{
                    data : action.payload.data,
                    total : action.payload.total,
                    loading : action.payload.loading
                }
            };
        }
        case EUserTestActions.GetUserTestsDetailsSuccess: {
            return {
                ...state,
                userTestsDetails:{
                    data : action.payload.data,
                    total : action.payload.total,
                    loading : action.payload.loading
                }
            };
        }
        case EUserTestActions.GetTestingHistorySuccess: {
            return {
                ...state,
                testingHistory:{
                    data : action.payload.data,
                    total : action.payload.total,
                    loading : action.payload.loading,
                    testName : action.payload.testName,
                    testScore: action.payload.testScore,
                    testId: action.payload.testId
                }
            };
        }
        case EUserTestActions.GetUserTestingListForCheckSuccess: {
            return {
                ...state,
                userTestingListForCheck:{
                    data : action.payload.data,
                    total : action.payload.total,
                    loading : action.payload.loading
                }
            };
        }

        default:
            return state;
    }
}