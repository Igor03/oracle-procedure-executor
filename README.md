# Oracle Procedure Executor
Here we have a simple solution for a problem that I faced a couple of times: running Oracle procedures using Entity Framework Core. There's probably a better way to achieve the same results but, the one I present here, worked for me and it can be adapted to different scenarios.


## Install & Run

Fisrt, you have to clone the files in this repository to your local machine in order to run them. To do this, simply run the following on your termninal:

``` git clone https://github.com/Igor03/oracle-procedure-executor.git ```

Then, with the project in hand, make you'll have to create all the Oracle Database resources, i.e., the sample procedures that were used to test this solution. We have 3 different procedures with different purposes and characteristics. Below we have all three procedures with its definitions and characteristics.

### Procedure 1: ```get_next_value```
This procedure simply returns the next value of an user defined sequence. In case you want to simulate the exactly same scenarios, I'll be showing the DDL code to create this sequence.

```sql 
CREATE SEQUENCE MY_SEQUENCE
	START WITH     1000
	INCREMENT BY   1
	NOCACHE
	NOCYCLE;	
```
Following, we have the ``` get_next_value ``` procedure DDL code.

```sql 
PROCEDURE get_next_value (	
	outNextValue	OUT INTEGER
)
IS 
	BEGIN 
		SELECT MY_SEQUENCE.NEXTVAL
		INTO outNextValue
		FROM DUAL
	END;
END; 	
```
### Procedure 2: ```math_operations```
This stored procedure accepts, as input parameters, two values of type ``` decimal ``` (``` NUMBER ``` for PL/SQL) and returns all four basic math operations applied using them. Different of the procedure presented before, this one has both input (```IN```) and output (```OUT```) parameters.

The DDL code for this procedure is shown bellow.

```sql
PROCEDURE math_operations (
	inNumber1	IN  NUMBER,
	inNumber2	IN  NUMBER,
	outOperation1	OUT NUMBER,
	outOperation2	OUT NUMBER,
	outOperation3	OUT NUMBER, 
	outOperation4	OUT NUMBER 
)
IS
	BEGIN 
		SELECT (inNumber1 + inNumber2)
		INTO outOperation1
		FROM DUAL;

		SELECT (inNumber1 - inNumber2)
		INTO outOperation2
		FROM DUAL;

		SELECT (inNumber1 * inNumber2)
		INTO outOperation3
		FROM DUAL;

		SELECT (inNumber1 / inNumber2)
		INTO outOperation4
		FROM DUAL
	END; 
END;
```

### Procedure 3: ```apply_taxes```

The last sample procedure is called ```apply_taxes``` and its goal is, based on a product type, it updates the value of this product, applying different rates. This procedure accepts, as input/output parameters, two [Associative Arrays](https://www.oracletutorial.com/plsql-tutorial/plsql-associative-array/), which represents the prices of the products and types of those products, respectively.

The DDL code for this procedure is shown bellow.

```sql
PROCEDURE apply_taxes (
	inProductOriginalPriceArray	IN OUT  NumberAssocArray,
	inProductTypeArray		IN OUT  VarcharAssocArray
)
IS
taxRateType1 NUMBER := 1.1;
taxRateType2 NUMBER := 1.2;
taxRateType3 NUMBER := 1.3;
BEGIN
	FOR idx IN 1..inProductOriginalPriceArray.COUNT 
	LOOP
		IF inProductTypeArray(idx) = 'Type1' THEN
			inProductOriginalPriceArray(idx) := inProductOriginalPriceArray(idx) * taxRateType1;
		ELSIF inProductTypeArray(idx) = 'Type2' THEN
			inProductOriginalPriceArray(idx) := inProductOriginalPriceArray(idx) * taxRateType2;
		ELSIF inProductTypeArray(idx) = 'Type3' THEN
			inProductOriginalPriceArray(idx) := inProductOriginalPriceArray(idx) * taxRateType3;
		ELSE
			dbms_output.put_line('Invalid product type.');
		END IF;
	END LOOP;
END;
```

## Important
This application uses [Visual Studio Secrets Manager](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows) to store secrets locally. For instance, we are not showing out database connection string. Hence, you'll have to configure it in your own environment.

## Contact
In case you need any help running/understanding this, feel free to contact me and I'll try to help you the best way I can.
