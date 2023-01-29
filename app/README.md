# Getting Started

This project was bootstrapped with [Create React App](https://github.com/facebook/create-react-app) and uses Bootstrap.

## Run Server-side

Open the solution file "PaylocityBenefitsCalculatorSeed\PaylocityBenefitsCalculator\PaylocityBenefitsCalculator.sln" in Visual Studio 2022;
In Test Explorer run unit tests from ApiTests project and make sure that all of them are green
Run the Api project in the Debugger (IIS Express)
A Swagger "Employee Benefit Cost Calculation Api" page should show up in a browser - do not close the page!

## Run Server-side

In Command Promplt open project app folder: PaylocityBenefitsCalculatorSeed\app>
Run `npm start`

## Updating employee

At this moment there is a limitation at the functionality of creating a new employee - the dependencies may be added only to existing employee, so first create an employee and then add / edit the employee’s dependents.
On Edit Employee dialog update the dependencies information and click "Update dependents" to save changes.
Click "Save employee" to update last name, first name and salary for the selected employee


## Available Scripts

In the project directory, you can run:

### `npm start`

Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in your browser.

The page will reload when you make changes.\
You may also see any lint errors in the console.

### `npm run build`

Builds the app for production to the `build` folder.\
It correctly bundles React in production mode and optimizes the build for the best performance.

The build is minified and the filenames include the hashes.\
Your app is ready to be deployed!

See the section about [deployment](https://facebook.github.io/create-react-app/docs/deployment) for more information.

### `npm run eject`

**Note: this is a one-way operation. Once you `eject`, you can't go back!**

If you aren't satisfied with the build tool and configuration choices, you can `eject` at any time. This command will remove the single build dependency from your project.

Instead, it will copy all the configuration files and the transitive dependencies (webpack, Babel, ESLint, etc) right into your project so you have full control over them. All of the commands except `eject` will still work, but they will point to the copied scripts so you can tweak them. At this point you're on your own.

You don't have to ever use `eject`. The curated feature set is suitable for small and middle deployments, and you shouldn't feel obligated to use this feature. However we understand that this tool wouldn't be useful if you couldn't customize it when you are ready for it.