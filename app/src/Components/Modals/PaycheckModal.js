import { currencyFormat } from "../Constants";

const PaycheckModal = (props) => {
    return (
        <div className="modal fade" id="paycheck-modal" tabIndex="-1" aria-labelledby="paycheck-modal-label" aria-hidden="true">
            <div className="modal-dialog">
                <div className="modal-content">
                    <div className="modal-header">
                        <h1 className="modal-title fs-5" id="paycheck-modal-label">Employee Paycheck</h1>
                        <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div className="modal-body">
                        <h5 className="text-left text-primary">{props.employee.firstName} {props.employee.lastName}</h5>
                        <dl className="row">
                            <dt className="col-sm-6">Salary (annual):</dt>
                            <dd className="col-sm-6">{currencyFormat(props.employee.salary)}</dd>

                            <dt className="col-sm-6">Pay Frequency:</dt>
                            <dd className="col-sm-6">Bi-weekly</dd>
                            
                            <dt className="col-sm-6">Gross:</dt>
                            <dd className="col-sm-6">{currencyFormat(props.paycheck.gross)}</dd>
                            
                            <dt className="col-sm-6">Deductions:</dt>
                            <dd className="col-sm-6">({currencyFormat(props.paycheck.totalDeductions)})</dd>
                            
                            <dt className="col-sm-6">NetPay:</dt>
                            <dd className="col-sm-6">{currencyFormat(props.paycheck.netPay)}</dd>
                        </dl>
                        <table className="table caption-top">
                            <thead className="table-dark">
                                <tr>
                                    <th scope="col">Deduction (hover for description)</th>
                                    <th scope="col">Amount</th>
                                </tr>
                            </thead>
                            <tbody>
                                {props.paycheck?.deductions?.map(({name, cost, description}) => (
                                    // Using index as a key causes an error in Chrome
                                    <tr key={name}>
                                        <td data-toggle="tooltip" title={description}>{name}</td>
                                        <td>({currencyFormat(cost)})</td>
                                    </tr>
                                ))}
                            </tbody>
                            </table>
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default PaycheckModal;