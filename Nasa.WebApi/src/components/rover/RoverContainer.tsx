import { ErrorData, fetchCommon } from '../../common/common';
import { Coordinate, MovementResult } from '../../models/rover.model';
import React, { useCallback, useEffect, useState } from 'react';
import RoverComponent, { RoverMovement } from './RoverComponent';



const arrangeMovements = (rovers: RoverMovement[], setRovers: React.Dispatch<React.SetStateAction<RoverMovement[]>>): void => {

    let i = 1;
    for (let rov of rovers) {
        for (let mov of rov.coordinates) {
            setTimeout(() => {
                const newRov = { ...rov, currentCoordinates: mov }
                const newRovers = rovers.filter(item => item.id !== rov.id);
                setRovers([...newRovers, newRov]);
            }, i * 1000)
            i++;
        }
    }
}



const MainContainer: React.FC = () => {

    const [fileName, setFileName] = useState('');
    const [rovers, setRovers] = useState<RoverMovement[]>([]);
    const [plateau, setPlateau] = useState<Coordinate>({ x: 5, y: 5 });
    const [err, setErr] = useState<ErrorData>({ message: "" });



    useEffect(() => {
        console.log(rovers);
    }, [rovers])


    const handleChange = useCallback(async (e: React.ChangeEvent<HTMLInputElement>): Promise<void> => {
        console.log(e.target.files);
        if (!e.target.files) {
            return;
        }

        const fileSelected = e.target.files[0]
        const formData = new FormData();
        formData.append("data", fileSelected, fileSelected.name);
        const options = {
            method: "POST",
            body: formData
        };
        const result = await fetchCommon<MovementResult>("https://localhost:44311/api/v1/Movement/handlefile", options);
        if ('plateau' in (result.data ?? {})) {
            const res = result.data as MovementResult;
            setFileName(fileSelected.name);
            setErr({ message: "" });
            if (res?.rovers) {

                const initials = res.rovers.map((item, index) => ({ id: index, currentCoordinates: item.coordinates[0], coordinates: item.coordinates } as RoverMovement));
                arrangeMovements(initials, setRovers);
                setPlateau(res?.plateau);
            }
        }
        else {
            setFileName('');
            setErr({ message: "" });
            if ('message' in (result.data ?? {})) {
                const res = result.data as ErrorData;
                setErr(res);
            }
        };
    }, [])

    return <div className="rover-container">
        <div className="rover">
            <input className="rover__file-input" type="file" onChange={handleChange} />
            <span className="rover__file-name">{fileName}</span>
        </div>
        <RoverComponent
            plateau={plateau}
            rovers={rovers}
        />
        {err.message && <div className="rover__error">
            <span>{err.message}</span>
        </div>}
    </div>
}


export default MainContainer;