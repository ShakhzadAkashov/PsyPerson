import { ETestActions, TestActions } from "../actions/test.actions";
import { initialTestState, TestState } from "../state/test.state";

export const testReducers = (
    state = initialTestState,
    action: TestActions
): TestState => {
    switch(action.type){
        case ETestActions.GetTestsSuccess: {
            return {
                ...state,
                tests:{
                    data : action.payload.data,
                    total : action.payload.total,
                    loading : action.payload.loading
                }
            };
        }
        case ETestActions.GetTestSuccess: {
            return {
                ...state,
                selectedTest: action.payload
            };
        }
        case ETestActions.GetTestQuestionsSuccess: {
            return {
                ...state,
                testQuestions:{
                    data : action.payload.data,
                    total : action.payload.total,
                    loading : action.payload.loading
                }
            };
        }
        case ETestActions.GetTestQuestionSuccess: {
            return {
                ...state,
                selectedTestQuestion: action.payload
            };
        }
        case ETestActions.GetTestForTestingSuccess: {
            return {
                ...state,
                testForTesting: action.payload
            };
        }

        default:
            return state;
    }
}