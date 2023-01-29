const DeleteEmployeeModal = (props) => {
    return (
        <div className="modal fade" id="delete-employee-modal" tabIndex="-1" aria-labelledby="delete-employee-modal-label" aria-hidden="true">
            <div className="modal-dialog">
                <div className="modal-content">
                    <div className="modal-header">
                        <h1 className="modal-title fs-5" id="delete-employee-modal-label">Delete Employee</h1>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div className="modal-body">
                        <p>Are you sure you would like to delete {props.employee.firstName} {props.employee.lastName}?</p>
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                        <button type="button" className="btn btn-primary" data-bs-dismiss="modal" onClick={props.deleteEmployee}>OK</button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default DeleteEmployeeModal;