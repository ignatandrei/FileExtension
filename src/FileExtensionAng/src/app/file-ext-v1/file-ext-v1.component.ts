import { Component, OnInit } from '@angular/core';
import { FileExtV1Service } from '../services/FileExtv1.service';

@Component({
  selector: 'app-file-ext-v1',
  templateUrl: './file-ext-v1.component.html',
  styleUrls: ['./file-ext-v1.component.css']
})
export class FileExtV1Component implements OnInit {

  extensions:string[] =[];
  constructor(private svc: FileExtV1Service) { }

  ngOnInit(): void {
    this.svc.GetExtensionsRecognized().subscribe(
      data => {
        this.extensions=data;
      },
      err => {
        console.log(err);
      }
    );  
  }
  uploadFile(file:FileList| null){
    if(file == null || file.length == 0)
      return;
    var f=file[0];
    this.svc.VerifyFile(f).subscribe(
      data => {
        window.alert("Result: "+data);
      },
      err => {
        console.log(err);
      }
    );
  }

}
