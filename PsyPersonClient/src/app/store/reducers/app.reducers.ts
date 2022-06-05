import { routerReducer } from "@ngrx/router-store";
import { ActionReducer, ActionReducerMap, MetaReducer } from "@ngrx/store";
import { environment } from "src/environments/environment";
import { AppState } from "../state/app.state";
import { roleReducers } from "./role.reducers";
import { suggestionReducers } from "./suggestion.reducers";
import { testReducers } from "./test.reducers";
import { userReducers} from "./user.reducers";
import { userTestReducers } from "./userTest.reducers";

export const appReducers: ActionReducerMap<AppState, any> = {
    router: routerReducer,
    users: userReducers,
    roles:roleReducers,
    tests:testReducers,
    userTests: userTestReducers,
    suggestions: suggestionReducers
};

export function logger(reducer: ActionReducer<AppState>): ActionReducer<AppState> {
    return (state, action) => {
      const result = reducer(state, action);
    //   console.groupCollapsed(action.type);
      console.log('prev state', state);
      console.log('action', action);
      console.log('next state', result);
    //   console.groupEnd();
  
      return result;
    };
  }

  export const metaReducers: MetaReducer<AppState>[] = !environment.production
  ? [logger]
  : [];