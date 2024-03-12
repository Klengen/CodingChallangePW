import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ProjectTableComponent} from "./project-table/project-table.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, ProjectTableComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'CodingChallange';
}
