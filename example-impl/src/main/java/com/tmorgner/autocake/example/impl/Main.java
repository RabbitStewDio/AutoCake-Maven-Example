package com.tmorgner.autocake.example.impl;

import com.tmorgner.autocake.example.api.HelloWorldService;

public class Main {

  public static void main(String[] args) {
    HelloWorldService hs = new BasicHelloWorldService();
    System.out.println(hs.getGreeting(System.getProperty("user.name", "Anonymous")));
    System.exit(0);
  }
}
