import { useState, useEffect } from 'react';
import Employee from './Employee';
import AddEmployeeModal from './Modals/AddEmployeeModal';
import PaycheckModal from './Modals/PaycheckModal';
import DeleteEmployeeModal from './Modals/DeleteEmployeeModal'
import Service from '../Service'
import { baseUrl, toDate, currencyToNumber } from "./Constants";

const EmployeeListing = () => {
    const [employees, setEmployees] = useState([]);
    const [error, setError] = useState(null);
    const [selectedEmployee, setSelectedEmployee] = useState([]);
    const [paycheckInfo, setPaycheckInfo] = useState([]);
    const [isAddingEmployee, setIsAddingEmployee] = useState([false]);
    const addEmployeeModalId = "add-employee-modal";
    const paycheckModalId = "paycheck-modal";
    const deleteEmployeeModalId = "delete-employee-modal";
    const service = new Service();

    useEffect(() => {
        getEmployees();
    }, []);

    const addEmployeeHandler = () => {
        setSelectedEmployee([]);
        setIsAddingEmployee(true);
    }

    // Server-side calls
    const getEmployees = () => {
        const url = `${baseUrl}/api/v1/Employees`;
        service.get(url, setEmployees, setError);
    }

    const calculatePaycheck = (props) => {
        const url = `${baseUrl}/api/v1/Paycheck/${props.id}`;
        service.get(url, setPaycheckInfo, setError);
    }

    const saveEmployee = () => {
        selectedEmployee.salary = currencyToNumber(selectedEmployee.salary);
        selectedEmployee.dateOfBirth = toDate(selectedEmployee.dateOfBirth);


        if (isAddingEmployee) {
            const url = `${baseUrl}/api/v1/Employees`;
            service.post(url, selectedEmployee, getEmployees, setError);
        } else {
            const url = `${baseUrl}/api/v1/Employees/${selectedEmployee.id}`;
            service.put(url, selectedEmployee, getEmployees, setError);
        }
    }

    const deleteEmployee = () => {
        const url = `${baseUrl}/api/v1/Employees/${selectedEmployee.id}`;
        service.delete(url, getEmployees, setError);
    }

    return (
        <div className="employee-listing">
            <table className="table caption-top">
                <caption>Employees</caption>
                <thead className="table-dark">
                    <tr>
                        <th scope="col">Id</th>
                        <th scope="col">Last Name</th>
                        <th scope="col">First Name</th>
                        <th scope="col">DOB</th>
                        <th scope="col">Salary</th>
                        <th scope="col">Dependents</th>
                        <th scope="col">Actions</th>
                    </tr>
                </thead>
                <tbody>
                {employees.map(({id, firstName, lastName, dateOfBirth, salary, dependents}) => (
                    <Employee
                        key={id}
                        id={id}
                        firstName={firstName}
                        lastName={lastName}
                        dateOfBirth={dateOfBirth}
                        salary={salary}
                        dependents={dependents}
                        editModalId={addEmployeeModalId}
                        paycheckModalId={paycheckModalId}
                        deleteModalId={deleteEmployeeModalId}
                        setEmployee={setSelectedEmployee}
                        calculatePaycheck={calculatePaycheck}
                        setIsAddingEmployee={setIsAddingEmployee}
                    />
                ))}
                </tbody>
            </table>
            <button type="button" className="btn btn-primary" data-bs-toggle="modal" onClick={addEmployeeHandler} data-bs-target={`#${addEmployeeModalId}`}>Add Employee</button>

            <AddEmployeeModal
                id={addEmployeeModalId} 
                employee={selectedEmployee}
                updateEmployee={setSelectedEmployee}
                saveEmployee={saveEmployee}
                isAddingEmployee={isAddingEmployee}
            />
            <PaycheckModal
                id={paycheckModalId} 
                employee={selectedEmployee}
                paycheck={paycheckInfo}
            />
            <DeleteEmployeeModal
                id={deleteEmployeeModalId} 
                employee={selectedEmployee}
                deleteEmployee={deleteEmployee}
            />

            <h6 className="text-danger">{error}</h6>
        </div>
    );
};

export default EmployeeListing;