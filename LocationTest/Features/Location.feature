Feature: Location

Background: 
	Given following existing clients
	| Username | Password | Birth      | LicenseNumber  | License    |
	| clement  | 12345    | 02/05/1994 | 49561          | 05/06/2016 |
	| edgar    | 48263    | 04/06/2005 | 0			    | 01/01/2000 |
	| paul     | 19547    | 12/02/1998 | 0			    | 01/01/2000 |

	Given following cars
	| Registration | Mark    | Model     | Color            | ReservationPrice | KilometerRate | Horsepower |
	| FM542AQ      | TOYOTA  | YARIS III | bleu             | 200              | 0.50          | 8          |
	| EF248PA      | FORD    | KA +      | gris             | 150              | 0.75          | 8          |
	| AE167TM      | PEUGEOT | 2008      | gris clair metal | 300              | 1.10          | 13         |
	| IJ425QD      | MINI    | MINI III  | bleu             | 500              | 1.25          | 16         |
	
Scenario: Client connection - Username not recognized
	Given my username is "clem"
	And my password is "12345"
	When I try to connect to my account
	Then the connection is refused
	And the error message is "Username not recognized"

Scenario: Client connection - Username recognized
	Given my username is "clement"
	And my password is "12345"
	When I try to connect to my account
	Then the connection is established

Scenario: Client connection - Username recognized but incorrect password
	Given my username is "clement"
	And my password is "54321"
	When I try to connect to my account
	Then the connection is refused
	And the error message is "Incorrect password"

Scenario: Client reservation - Reservation between a client and a vehicle
	Given my username is "clement"
	And my password is "12345"
	When I try to connect to my account
	Then the connection is established
	Given following location dates
		| StartDate  | EndDate    |
		| 04/05/2021 | 07/05/2021 |
	When set location dates
	Then the vehicules list should be
		| Registration | Mark    | Model     | Color            | ReservationPrice | KilometerRate | Horsepower |
		| FM542AQ      | TOYOTA  | YARIS III | bleu             | 200              | 0.50		   | 8          |
		| EF248PA      | FORD    | KA +      | gris             | 150              | 0.75          | 8          |
		| AE167TM      | PEUGEOT | 2008      | gris clair metal | 300              | 1.10          | 13         |
		| IJ425QD      | MINI    | MINI III  | bleu             | 500              | 1.25          | 16         |
	Given the vehicule is "AE167TM"
	And estimate km to "150"
	When set location
	Then the location should be
		| Username | Registration | StartDate  | EndDate    | EstimateKm | Price |
		| clement  | AE167TM      | 04/05/2021 | 07/05/2021 | 150        | 630   |

Scenario: Client reservation - Reservation between a client and a vehicle - 18
	Given my username is "edgar"
	And my password is "48263"
	When I try to connect to my account
	Then the connection is established
	Given following location dates
		| StartDate  | EndDate    |
		| 04/05/2021 | 07/05/2021 |
	When set location dates
	Then the vehicules list should be
		| Registration | Mark    | Model     | Color            | ReservationPrice | KilometerRate | Horsepower |
		| FM542AQ      | TOYOTA  | YARIS III | bleu             | 200              | 0.50		   | 8          |
		| EF248PA      | FORD    | KA +      | gris             | 150              | 0.75          | 8          |
	Given the vehicule is "FM542AQ"
	When set location
	Then the error message is "-18"

Scenario: Client reservation - Reservation between a client and a vehicle - No License
	Given my username is "paul"
	And my password is "19547"
	When I try to connect to my account
	Then the connection is established
	Given following location dates
		| StartDate  | EndDate    |
		| 04/05/2021 | 07/05/2021 |
	When set location dates
	Then the vehicules list should be
		| Registration | Mark    | Model     | Color            | ReservationPrice | KilometerRate | Horsepower |
		| FM542AQ      | TOYOTA  | YARIS III | bleu             | 200              | 0.50		   | 8          |
		| EF248PA      | FORD    | KA +      | gris             | 150              | 0.75          | 8          |
		| AE167TM      | PEUGEOT | 2008      | gris clair metal | 300              | 1.10          | 13         |
	Given the vehicule is "FM542AQ"
	When set location
	Then the error message is "Invalid license"

Scenario: Client reservation - Reservation between a client and a vehicle - Too location
	Given my username is "clement"
	And my password is "12345"
	When I try to connect to my account
	Then the connection is established
	Given following location dates
		| StartDate  | EndDate    |
		| 04/05/2021 | 07/05/2021 |
	When set location dates
	Then the vehicules list should be
		| Registration | Mark    | Model     | Color            | ReservationPrice | KilometerRate | Horsepower |
		| FM542AQ      | TOYOTA  | YARIS III | bleu             | 200              | 0.50		   | 8          |
		| EF248PA      | FORD    | KA +      | gris             | 150              | 0.75          | 8          |
		| AE167TM      | PEUGEOT | 2008      | gris clair metal | 300              | 1.10          | 13         |
		| IJ425QD      | MINI    | MINI III  | bleu             | 500              | 1.25          | 16         |
	Given the vehicule is "AE167TM"
	And estimate km to "150"
	When set location
	Then the location should be
		| Username | Registration | StartDate  | EndDate    | EstimateKm | Price |
		| clement  | AE167TM      | 04/05/2021 | 07/05/2021 | 150        | 630   |
	Given following location dates
		| StartDate  | EndDate    |
		| 04/05/2021 | 07/05/2021 |
	When set location dates
	Then the vehicules list should be
		| Registration | Mark    | Model     | Color            | ReservationPrice | KilometerRate | Horsepower |
		| FM542AQ      | TOYOTA  | YARIS III | bleu             | 200              | 0.50		   | 8          |
		| EF248PA      | FORD    | KA +      | gris             | 150              | 0.75          | 8          |
		| AE167TM      | PEUGEOT | 2008      | gris clair metal | 300              | 1.10          | 13         |
		| IJ425QD      | MINI    | MINI III  | bleu             | 500              | 1.25          | 16         |
	Given the vehicule is "FM542AQ"
	And estimate km to "300"
	When set location
	Then the error message is "Too location 'AE167TM'"