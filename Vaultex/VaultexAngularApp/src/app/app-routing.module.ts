import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AppComponent} from "./app.component";
import {EmployeesComponent} from "./employees/employees.component";
import {OrganisationsComponent} from "./organisations/organisations.component";

const routes: Routes = [
  {path: '', component: AppComponent},
  {path: 'employees', component: EmployeesComponent},
  {path: 'organisations', component: OrganisationsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
