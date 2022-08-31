docker run -d \
    --name exchange-rate \
    -p 5001:80 \
    -e PostgresConnection="User ID=postgres;Password=postgres;Server=192.168.2.194;Port=5432;Database=currency-exchange;" \
    exchange-rate 
