import { dateFormat } from "./Constants";

const Dependent = (props) => {
  const dpnd = props.dependent;

  return (
    <tr>
        <td>
          <input type="text" className="form-control" id="lastName" placeholder="Last Name" 
              value={dpnd.lastName} onChange={(e) => props.setDependent({...dpnd, lastName: e.target.value})} />
        </td>
        <td>
          <input type="text" className="form-control" id="firstName" placeholder="First Name" 
              value={dpnd.firstName} onChange={(e) => props.setDependent({...dpnd, firstName: e.target.value})} />
        </td>
        <td>
          <input type="text" className="form-control" id="dob" placeholder="DOB" 
              value={dateFormat(dpnd.dateOfBirth)} onChange={(e) => props.setDependent({...dpnd, dateOfBirth: e.target.value})} />
        </td>
        <td>
          <select className="form-select" id="relationship" 
              value={dpnd.relationship} onChange={(e) => props.setDependent({...dpnd, relationship: e.target.value})}>
            <option value="0">None</option>
            <option value="1">Spouse</option>
            <option value="2">Domestic Partner</option>
            <option value="3">Child</option>
          </select>
        </td>
        <td>
          <button type="button" className="btn btn-close btn-sm" aria-label="Close" onClick={() => props.removeDependent(dpnd.id)} />
        </td>
    </tr>
  );
};

export default Dependent;