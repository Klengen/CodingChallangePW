import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { HttpClientModule } from '@angular/common/http';
import { HttpClient } from '@angular/common/http';
import {MatSort, MatSortModule} from '@angular/material/sort';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {provideNativeDateAdapter} from '@angular/material/core';
import { DatePipe } from '@angular/common';
import {MatIcon} from "@angular/material/icon";
@Component({
  selector: 'app-project-table',
  standalone: true,
  providers: [provideNativeDateAdapter()],
  imports: [MatSortModule, HttpClientModule, MatTableModule, MatPaginatorModule, MatFormFieldModule, MatInputModule, MatDatepickerModule, DatePipe, MatIcon],
  templateUrl: './project-table.component.html',
  styleUrl: './project-table.component.css'
})
export class ProjectTableComponent  implements OnInit,AfterViewInit {
  constructor(private http: HttpClient) { }
  projects: Project[] = [];
  startDate = new Date(1990, 0, 1);
  endDate = new Date(1990, 0, 1);
  displayedColumns: string[] = ['city', 'startDate', 'endDate', 'price', 'status', 'color'];
  dataSource = new MatTableDataSource<Project>(this.projects);

  ngOnInit() : void{
    this.http.get<Project[]>("http://localhost:5000/project").subscribe((data) => {
      this.projects = data;
      this.dataSource.data = this.projects;
    })
  }
  ngAfterViewInit() {
    if (this.paginator) {
      this.dataSource.paginator = this.paginator;
    }
    if (this.sort) {
      this.dataSource.sort = this.sort;
    }

  }
  @ViewChild(MatPaginator) paginator: MatPaginator | undefined;
  @ViewChild(MatSort) sort: MatSort | undefined;

  saveStartDate(event: any){
    this.startDate = event.value;
  }
  saveEndDate(event: any){
    console.log('End Date:', event.value);
    this.endDate = event.value;
    console.log('this.endDate:', this.endDate);
    this.filterByDate();
  }

  filterByDate(){
    console.log(this.endDate)
    if(this.endDate==null || this.startDate==null){
      this.dataSource.data = this.projects
    }else {
      var newProjects: Project[] = [];
      this.dataSource.data.forEach((project: Project) => {
        if(this.endDate>new Date(project.endDate) && this.startDate<new Date(project.startDate)){
          newProjects.push(project)
        }
      })
      this.dataSource.data = newProjects;
    }

  }

//  protected readonly Status = Status;
}


export interface Project {
  id: number,
  city: string,
  startDate: Date,
  endDate: Date,
  price: number,
  status: string,
  color: string
}

/*export enum Status {
  Never,
  Once,
  Seldom,
  Often,
  Yearly,
  Monthly,
  Weekly,
  Daily
}*/

