import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CRUDExample } from 'src/app/models/CRUDExample';
import { ApiService } from 'src/app/services/api.service';
import { Location } from '@angular/common';

@Component({
  selector: 'crud-example',
  templateUrl: './crud-example.component.html',
  styleUrls: ['./crud-example.component.scss']
})
export class CrudExampleComponent implements OnInit {

  model: CRUDExample = new CRUDExample();
  
  actionTitle: string = ""
  constructor(private activatedRoute: ActivatedRoute, private api: ApiService, private location: Location, private router: Router) { }

  ngOnInit(): void {
    let id : number = +this.activatedRoute.snapshot.params['id'];
    this.actionTitle = id && id !== 0 ? "Update" : "Create";
    if (id && id !== 0) 
      this.load(id);
  }

  async load(id: number) {
    this.model = await this.api.getCrudExample(id);
  }

  back(){
    this.location.back();
  }

  async save(){
    const result = await (this.model.id === 0) ? this.api.createCrudExample(this.model) : this.api.updateCrudExample(this.model);
    this.router.navigate(['/crud-examples']);
  }
}
