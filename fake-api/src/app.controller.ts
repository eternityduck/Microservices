import { Controller, Get } from '@nestjs/common';
import { AppService } from './app.service';

@Controller({
  path: '/fake-api'
})
export class AppController {
  constructor(private readonly appService: AppService) {}

  @Get('/hello')
  getHello(): string {
    return this.appService.getHello();
  }
}
