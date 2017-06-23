package com.tmorgner.autocake.example.impl;

import com.tmorgner.autocake.example.api.HelloWorldService;

public class BasicHelloWorldService implements HelloWorldService {
  @Override
  public String getGreeting(String name) {
    return String.format("Hello World! Now go away, %s!", name);
  }
}
