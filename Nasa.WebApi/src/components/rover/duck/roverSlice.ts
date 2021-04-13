import { createSlice } from '@reduxjs/toolkit';

interface RoverState {
    id: number
}

const initialState = {
    id: 0
} as RoverState;


const roverSlice = createSlice({
    name: 'table',
    initialState,
    reducers: {
    }

})

export default roverSlice.reducer;