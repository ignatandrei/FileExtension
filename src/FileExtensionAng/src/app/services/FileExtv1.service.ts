import { HttpClient, HttpEventType } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FileExtV1Service {
  constructor(private http: HttpClient) { }
  
  VerifyFile(fileToUpload: File) : Observable<string> {
    var url =environment.url +'api/v1/RecognizeFile/IsCorrectExtensionSendFile';
    const formData = new FormData();
    formData.append('file', fileToUpload, fileToUpload.name);
    var ret= this.http.post<string>(url, formData, /*{reportProgress: true, observe: 'events'}*/)
    // .subscribe(event => {
    //   if (event.type === HttpEventType.UploadProgress)
    //     this.progress = Math.round(100 * event.loaded / event.total);
    //   else if (event.type === HttpEventType.Response) {
    //     this.message = 'Upload success.';
    //     this.onUploadFinished.emit(event.body);
    //   }
    // })
    ;
    return ret;
  }

  GetExtensionsRecognized(): Observable<string[]> {
    var url =environment.url +'api/v1/RecognizeFile/GetExtensionsRecognized';
    return this.http.get<string[]>(url);
  }
  
}