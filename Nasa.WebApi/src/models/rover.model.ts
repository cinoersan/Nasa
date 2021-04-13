export interface Coordinate {
    x: number,
    y: number
}

export interface RoverStatus {
    heading: string,
    currentCoordinate: Coordinate,
    coordinates: Coordinate[]
    movementText: string
}

export interface MovementResult {
    plateau: Coordinate;
    rovers: RoverStatus[]
}