docker run -d \
    --name exchange-rate \
    -p 5001:80 \
    -e PostgresConnection="User ID=postgres;Password=postgres;Server=localhost;Port=5432;Database=currency-exchange;" \
    exchange-rate 
