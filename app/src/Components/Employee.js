import { currencyFormat } from "./Constants";
import { dateFormat } from "./Constants";

const Employee = (props) => {
    const firstName = props.firstName || '';
    const lastName = props.lastName || '';
    const salary = props.salary || 0;

    const editEmployeeHandler = () => {
        props.setIsAddingEmployee(false);
        props.setEmployee({...props});
    };

    const calculatePaycheckHandler = () => {
        props.setEmployee({...props});
        props.calculatePaycheck({...props});
    }

    const deleteEmployeeHandler = () => {
        props.setEmployee({...props});
    }
    
    return (
        <tr>
            <th scope="row">{props.id}</th>
            <td>{lastName}</td>
            <td>{firstName}</td>
            <td>{dateFormat(props.dateOfBirth)}</td>
            <td>{currencyFormat(salary)}</td>
            <td>{props.dependents?.length || 0}</td>
            <td>
                <a className="btn btn-link btn-sm" href="#" onClick={editEmployeeHandler} data-bs-toggle="modal" data-bs-target={`#${props.editModalId}`}>Edit</a>
                <a className="btn btn-link btn-sm" href="#" onClick={deleteEmployeeHandler} data-bs-toggle="modal" data-bs-target={`#${props.deleteModalId}`}>Delete</a>
                <a className="btn btn-link btn-sm" href="#" onClick={calculatePaycheckHandler} data-bs-toggle="modal" data-bs-target={`#${props.paycheckModalId}`}>Paycheck</a>
            </td>
        </tr>
    );
};

export default Employee;