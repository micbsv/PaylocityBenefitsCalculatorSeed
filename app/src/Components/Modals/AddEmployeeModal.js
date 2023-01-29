import { currencyFormat, dateFormat } from "../Constants";

const AddEmployeeModal = (props) => {
    const emp = props.employee;

    return (
        <div className="modal fade" id="add-employee-modal" tabIndex="-1" aria-labelledby="add-employee-modal-label" aria-hidden="true">
            <div className="modal-dialog">
                <div className="modal-content">
                    <div className="modal-header">
                        <h1 className="modal-title fs-5" id="add-employee-modal-label">
                            {props.isAddingEmployee ? 'Add' : 'Edit'} Employee
                        </h1>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div className="modal-body">
                        {/* <p>{JSON.stringify(emp.lastName)}</p> */}
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
                        </form>
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        <button type="button" className="btn btn-primary" data-bs-dismiss="modal" onClick={props.saveEmployee}>Save changes</button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default AddEmployeeModal;