import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CRUDExample } from 'src/app/models/CRUDExample';
import { Location } from '@angular/common';
import { GodService } from 'src/app/services/god.service';

@Component({
  selector: 'crud-example',
  templateUrl: './crud-example.component.html',
  styleUrls: ['./crud-example.component.scss']
})
export class CrudExampleComponent implements OnInit {

  model: CRUDExample = new CRUDExample();
  
  actionTitle: string = ""
  constructor(private activatedRoute: ActivatedRoute, private god: GodService, private location: Location) { }

  ngOnInit(): void {
    let id : number = +this.activatedRoute.snapshot.params['id'];
    this.actionTitle = id && id !== 0 ? "Update" : "Create";
    if (id && id !== 0) 
      this.load(id);
  }

  async load(id: number) {
    this.model = await this.god.api.getCrudExample(id);
  }

  back(){
    this.location.back();
  }

  async save(){
    this.god.notifications.success('saved!');
    this.god.router.navigate(['/crud-examples']);
  }
}
