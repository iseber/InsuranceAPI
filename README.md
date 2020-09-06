# InsuranceAPI

Insurance API is a service that uses Products API and calculates insurance cost regarding the product information

### Prerequisites 

You must have docker set up in your system  
You must have Products API ready and running on your http://localhost:5002

## Installation

On the project folder (Insurance.Api) run this command
```
docker-compose up
```

## Usage

When Docker container is up the service will start and seed required data to mongodb this can be queried by Mongo express from http://localhost:8081/  
Also API swagger documentation is accessible from http://localhost:5000/swagger/index.html

## Design Choices and Assumptions 

### Task 1 [BUGFIX]:
The financial manager reported that when customers buy a laptop that costs less than € 500, insurance is not calculated, while it should be € 500.

### Task 2 [REFACTORING]:
It looks like the already implemented functionality has some quality issues. Refactor that code, but be sure to maintain the same behavior. 
* Please make sure to include in the documentation about the approach that you chose for the refactoring.

The approaches that I took while refactoring this app are;
* Existing insurance calculation was static and hard-coded therefore I need more dynamic infrastructure to meet future needs for insurance calculation. 
I choose tho move all static values and if-else decision tree to database and service layer. The application does not have to be changed and re-deployed every time when we 
make a change for insurance costs and insurance calculation decision.
* I choose to change static `BusinessRules` to non-static, interface implemented so that brings me the flexibility of mocking the class and increase the testability of overall behaviour.
I inject `BusinessRules` class to DI container as Singleton as this class does not change for per-request. I merged `product-types` and `product` api requests under one method as we do not need separate methods to be able to gather the required data from `Products API`
* Insurance calculation move to a domain object `Insurance`. I choose to follow DDD when implementing `Insurance` class which encapsulates the business logic of calculating insurance costs.
* I implement repository pattern for database relational operations as we need for insurance, surcharge and order surcharge calculations. Repository has been registered to DI container as singleton as we need only one instance for database operations and not to create connections per request.
* Strongly typed confiugration classes created to have more control over configuration and enabled testing/mocking for unit tests.
* Request/Response models separated to have more control over what needs to be shown and hide over API endpoints and create more understandable end-user interfaces.
* Containarization implemented to easly build and run application with little effort.
* MongoDB nosql database solution choosen to have more control over calculation of insurance costs and surcharges. 

### Task 3 [FEATURE 1]:
Now we want to calculate the insurance cost for an order and for this, we are going to provide all the products that are in a shopping cart.
* While developing this feature, please document your assumptions and feel free to reach the stakeholders for doubts via email.

The existing product endpoint `api/insurance/product` was only accepting one product at a time for insurance calculation. My assumption is that an order may contain multiple products.
Therefore we need to calculate insurance costs per product and also sum of calculated insurance costs for order. To be able to achieve this I came up with an endpoint `api/insurance/order`
which accepts multiple products and in return calculates insurance costs per product and sum of all calculated insuraces for given order.

The logic which calculates insurance per product same as with order insurance endpoint. So I choose to move the same logic under `InsuranceDomainService` and iterate trough the product Ids 
and calculated insurance for each product. I collect each result under `OrderInsuranceResponseModel` and calculate sum of insurances for given order.

The order endpoint `api/insurance/order` accepts the below request model which contains the set of productids which needs insurance calculation.
```
public class OrderInsuranceRequestModel
{
    public List<int> ProductIds { get; set; }
}
```

Response of this endpoint will look like as below as this endpoint calculate insurance per product and sum of all calculated insurances per order
```
{
  "insuranceResponseModels": [
    {
      "productId": 859366,
      "insuranceValue": 1500
    },
    {
      "productId": 735296,
      "insuranceValue": 0
    },
    {
      "productId": 837856,
      "insuranceValue": 500
    },
    {
      "productId": 861866,
      "insuranceValue": 1500
    }
  ],
  "sumOfOrderInsurance": 3500,
  "orderSurcharge": 0
}
```


### Task 4 [FEATURE 2]:
We want to change the logic around the insurance calculation. We received a report from our business analysts that Drones are getting lost more than usual. Therefore, if an order has one or more drones, add € 500 to the insured value of the order.
* While developing this feature, please document your assumptions and feel free to reach the stakeholders for doubts via email.

In `api/insurance/order` endpoint I calculate insurance per product and sum of insurances for given order. To be able to meet business criteria in this request we need a special calculation just for product type `Drones`. For this business requirement (and for future requirements like this one) 
I choose to create a separate database table called `OrderSurcharges` to keep surcharge info for special products like `Drones`. After insurance calculation for an order, I keep productTypes in a list and send this list of distinct product types to my domain service `GetOrderSurcharge` 
so that I can collect surcharges based on productTypes and add that surcharges to sum of calculated insurance costs.

### Task 5 [FEATURE 3]:
As a part of this story we need to provide the administrators/back office staff with a new endpoint that will allow them to upload surcharge rates per product type. This surcharge will then  need to be added to the overall insurance value for the product type.

Please be aware that this endpoint is going to be used simultaneously by multiple users.
* While developing this feature, please document your assumptions and feel free to reach out to the stakeholders for doubts via email.

For administration purposes I create a separate controller and endpoint `api/insurance/surcharges` to host uploading multiple surcharges at the same time. 
I choose to implement endpoint functionality as async up until to database layer. So async all the way up enables you to actually make an asynchronous call and release any threads. If it isn't async all the way then some thread is being blocked.
Since this endpoint is going to be used simultaneously by multiple users it should not be blocked by any call. 

