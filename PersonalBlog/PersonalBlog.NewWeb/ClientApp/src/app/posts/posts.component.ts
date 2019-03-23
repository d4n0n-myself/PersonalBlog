import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html'
})
export class PostsComponent implements OnInit {
  public currentCount = 0;

  constructor(public http: HttpClient,
              @Inject('BASE_URL') baseUrl) {
    this.baseUrl = baseUrl;
  }

  baseUrl: string;
  posts = new Array<Post>();

  ngOnInit(): void {
    this.http.get<Post[]>(this.baseUrl + 'Posts/Show').pipe().subscribe(result => {
      this.posts = result as Post[];
    })
  }

  public incrementCounter() {
    this.currentCount++;
  }
}

interface Post {
  header : string;
  body : string;
}
