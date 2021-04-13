import React, { memo } from 'react';
import { Coordinate } from '../../models/rover.model';

export interface RoverMovement {
    id: number;
    currentCoordinates: Coordinate;
    coordinates: Coordinate[];
}

interface ComponentProps {
    rovers: RoverMovement[],
    plateau: Coordinate
}


const RoverComponent: React.FC<ComponentProps> = memo(props => {

    return <div className="plateau">
        <table className="plateau__table">
            <tbody className="plateau__body">
                {
                    Array(props.plateau.y + 1).fill(0).map((_, rowIndex) => {
                        return <tr key={rowIndex} className="plateau__row">
                            {
                                Array(props.plateau.x + 1).fill(0).map((_, columnIndex) => {
                                    const isHere = props.rovers.find(rov => rov.currentCoordinates.x == columnIndex && rov.currentCoordinates.y == rowIndex);

                                    return (
                                        <td key={columnIndex} className="plateau__column" >
                                            {
                                                !!isHere && <span className="plateau__times">&times;</span>
                                            }
                                        </td>
                                    )
                                })
                            }
                        </tr>
                    })
                }
            </tbody>
        </table>
    </div >
})



export default RoverComponent;