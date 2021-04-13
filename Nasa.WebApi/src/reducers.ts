
import roverSlice from './components/rover/duck/roverSlice';
import { combineReducers } from 'redux';


const rootReducer = combineReducers({
   rover: roverSlice
});

export type RootState = ReturnType<typeof rootReducer>;
export default rootReducer;