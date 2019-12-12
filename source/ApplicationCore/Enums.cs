namespace ApplicationCore
{
    public enum STATUS
    {
        AVAILABLE,
        DISABLED,
        ORDERED,
        PAID,
        DEPARTED
    }
    public enum ORDER_ENUM
    {
        //general
        ID = 0,     //primary key
        NAME = 1,  //fullname in person
        STATUS = 2,

        //order
        DESCENDING = 3,
        ASCENDING = 4,

        //for person
        FIRST_NAME = 5,
        LAST_NAME = 6,
        BIRTHDATE = 7,
        PHONE = 8,
        ADDRESS = 9,

        //for customer
        EMAIL = 10,

        //for employee
        JOB_NAME = 11,
        SALARY = 12,

        //for account
        PERSON_NAME = 13,
        PERSON_FIRST_NAME = 15,
        PERSON_LAST_NAME = 16,

        //for plane
        PLANE_NAME = 17,
        MAKER_NAME = 18,

        //for flight and route
        ORIGIN_NAME = 19,
        DESTINATION_NAME = 20,
        FLIGHT_TIME = 21,

        //for ticket
        CUSTOMER_NAME = 22,

        //for tickettype
        BASEPRICE = 23,

        //for flight
        DEP_DATE = 24, //departure date
        ARR_DATE = 25, //arrive date

        //for ticket
        TICKET_TYPE_ID = 26,
        ASSIGNED_CUSTOMER = 27

    }
}