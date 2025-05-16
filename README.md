# API_Task

Based on the UI/UX, there should be Register API, GenerateSendOTPToMobile API, GenerateSendOTPToEmail API, ValidationOTP API, CreatePIN API, Login API

For Register:

Register API: By this API, user submit his information and save to database and return user object including UserId(PK).

GenerateSendOTPToMobile API: If RegisterAPI IsSuccess = True, then mobile app call GenerateSendOTPToMobile API to generate OTP and send to user mobile.
"Note: I implemented the code to send OTP on mobile but credentials not shared, if required I can show it from my machine, it's working"

ValidationOTP API: If GenerateSendOTPToMobile IsSuccess = True, then user submit the OTP and ValidationOTP API will call to validate the OTP.

GenerateSendOTPToEmail API: If ValidationOTP IsSuccess = True, then mobile app call again GenerateSendOTPToEmail API to generate OTP and send to user email.
"Note: I implemented the code to send OTP on email but credentials not shared, if required I can show it from my machine, it's working"

ValidationOTP API: If GenerateSendOTPToEmail IsSuccess = True, then user submit the OTP and ValidationOTP API will call to validate the OTP.

CreatePIN API: If ValidationOTP IsSuccess = True, then user submit his PIN from mobile app and call CreatePIN API after check Privacy Policy and Confirm PIN and then he can access home page CreatePIN IsSuccess = True.

For login: 

Login API: For Login user submit his IC Number and call Login API. When Login API IsSuccess = True then GenerateSendOTPToMobile API, GenerateSendOTPToEmail API, ValidationOTP API, CreatePIN API
           will call one by one as like Registration.
		   
		   
Design Pattern: To implement this task project I used n tier layered architecture. Where,

Service layer: It's containing logic.
Repository layer: It's containing complexity (database connection, hiding concept, data pull/insert/update). 
Model layer containing objects, db entities, dbcontext. 
Configuration Layer: It's containing some configuration like email helper, dependency injection.
Main Project is the API's.

Entity Framework: I used Code First entity framework.


