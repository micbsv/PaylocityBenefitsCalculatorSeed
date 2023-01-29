import { useState } from 'react'
import { currencyFormat, dateFormat } from '../Constants';
import Dependent from '../Dependent'
import Service from '../../Service'
import { baseUrl, toDate } from "../Constants";

const modalStyle = {
    '--bs-modal-width': '600px'
}

const showDependentsStyle = {
    display: 'block'
}

const hideDependentsStyle = {
    display: 'none'
}

const AddEmployeeModal = (props) => {
    const [dependentsError, setDependentsError] = useState(null);
    const [dependentsSucceed, setDependentsSucceed] = useState(null);
    const [removedDepndents, setRemovedDepndents] = useState([]);
    let depndentSavedWithError = false;

    // Need this function to refresh Dependents part of the dialog after dependents changes
    // ref: https://tinyurl.com/2pope5g9
    const [value, setValue] = useState(-1);
    const useForceUpdate = () => {
        return () => setValue(value => value - 1); // update state to force render
    }

    const forceUpdate = useForceUpdate();
    const emp = props.employee;
    const service = new Service();

    const setDependent = (dependent) => {
        for (let i = 0; i < emp.dependents.length; i++) {
            let dpnd = emp.dependents[i];
            if (dpnd.id == dependent.id) {
                dpnd.lastName = dependent.lastName;
                dpnd.firstName = dependent.firstName;
                dpnd.dateOfBirth = dependent.dateOfBirth;
                dpnd.relationship = dependent.relationship;
            }
        }
        forceUpdate();
    }

    const removeDependent = (id) => {
        for (let i = 0; i < emp.dependents.length; i++) {
            let dpnd = emp.dependents[i];
        
            if (dpnd.id == id) {
                // Remove dependent from employee
                emp.dependents.splice(i, 1);

                // Collect removed dependents
                if (id > 0) {
                    removedDepndents.push(id);
                }
            }
        }
        forceUpdate();
    }

    const addDependent = () => {
        if (emp.dependents == null) {
            emp.dependents = [];    
        }

        const dependent = {
            id:  value, // Use value as a negative unique id to identify a new dependent
            firstName: '',
            lastName: '',
            dateOfBirth: '',
            relationship: 0
        };

        emp.dependents.push(dependent);
        forceUpdate();
    }

    const saveDependents = () => {
        resetSavingStatus();

        if (emp.dependents == null) {
            return;
        }

        for (let i = 0; i < emp.dependents.length; i++) {
            let dpnd = emp.dependents[i];

            dpnd.dateOfBirth = toDate(dpnd.dateOfBirth);
            dpnd.relationship = +dpnd.relationship;

            if (dpnd.id > 0) {
                // Update existing dependent
                const url = `${baseUrl}/api/v1/Dependents/${dpnd.id}`;
                service.put(url, dpnd, dependentSaved, dependentFailed);
            } else {
                // Add new dependent
                dpnd.employeeId = emp.id;
                const url = `${baseUrl}/api/v1/Dependents`;
                service.post(url, dpnd, dependentSaved, dependentFailed);
            }
        }

        for (let i = 0; i < removedDepndents.length; i++) {
            let id = removedDepndents[i];

            // Delete existing dependent
            const url = `${baseUrl}/api/v1/Dependents/${id}`;
            service.delete(url, dependentSaved, dependentFailed);
        }
    }

    const dependentFailed = (error) => {
        if (error != null && !depndentSavedWithError) {
            depndentSavedWithError = true;
            setDependentsSucceed(null);
            setDependentsError(error);
        }
    }

    const dependentSaved = () => {
        if (!depndentSavedWithError) {
            setDependentsSucceed('Dependents updated');
        }
    }

    const closeModal = () => {
        resetSavingStatus();
        setRemovedDepndents([]);
        props.getEmployees();
    }

    const saveEmployee = () => {
        resetSavingStatus();
        setRemovedDepndents([]);
        props.saveEmployee();
    }

    const resetSavingStatus = () => {
        setDependentsError(null);
        setDependentsSucceed(null);
        depndentSavedWithError = false;
    }

    return (
        <div className="modal fade" id="add-employee-modal" tabIndex="-1" aria-labelledby="add-employee-modal-label" aria-hidden="true">
            <div className="modal-dialog" style={modalStyle}>
                <div className="modal-content">
                    <div className="modal-header">
                        <h1 className="modal-title fs-5" id="add-employee-modal-label">
                            {props.isAddingEmployee ? 'Add' : 'Edit'} Employee
                        </h1>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div className="modal-body">
                        <form>
                            <div className="form-floating mb-3">
                                <input type="text" className="form-control" id="lastName" placeholder="Last Name" 
                                    value={emp.lastName} onChange={(e) => props.updateEmployee({...emp, lastName: e.target.value})} />
                                <label htmlFor="lastName">Last Name</label>
                            </div>
                            <div className="form-floating mb-3">
                                <input type="text" className="form-control" id="firstName" placeholder="First Name" 
                                    value={emp.firstName} onChange={(e) => props.updateEmployee({...emp, firstName: e.target.value})} />
                                <label htmlFor="firstName">First Name</label>
                            </div>
                            <div className="form-floating mb-3">
                                <input type="text" className="form-control" id="salary" placeholder="Salary" 
                                    value={currencyFormat(emp.salary)} onChange={(e) => props.updateEmployee({...emp, salary: e.target.value})} />
                                <label htmlFor="salary">Salary</label>
                            </div>
                            <div className="form-floating mb-3">
                                <input type="text" className="form-control" id="dob" placeholder="Date of birth" disabled={!props.isAddingEmployee}
                                    value={dateFormat(emp.dateOfBirth)} onChange={(e) => props.updateEmployee({...emp, dateOfBirth: e.target.value})} />
                                <label htmlFor="dob">Date of birth</label>
                            </div>
                            <div style={emp.id > 0 ? showDependentsStyle : hideDependentsStyle}>
                                <table className="table caption-top">
                                    <caption>Dependents</caption>
                                    <thead className="table-dark">
                                        <tr>
                                            <th scope="col">Last Name</th>
                                            <th scope="col">First Name</th>
                                            <th scope="col">DOB</th>
                                            <th scope="col" colSpan="2">Relationship</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {emp.dependents?.map((dependent) => (
                                            <Dependent
                                                key={dependent.id}
                                                dependent={dependent}
                                                setDependent={setDependent}
                                                removeDependent={removeDependent}
                                            />
                                        ))}
                                    </tbody>
                                </table>
                                <p className="text-danger">{dependentsError}</p>
                                <p className="text-success">{dependentsSucceed}</p>
                                <div>
                                    <button type="button" className="btn btn-outline-secondary m-1" onClick={addDependent}>Add</button>
                                    <button type="button" className="btn btn-outline-primary m-1" onClick={saveDependents}>Update dependents</button>
                                </div>
                            </div>
                        </form>
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary" data-bs-dismiss="modal" onClick={closeModal}>Close</button>
                        <button type="button" className="btn btn-primary" data-bs-dismiss="modal" onClick={saveEmployee}>Save employee</button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default AddEmployeeModal;