docker run -d \
    --name exchange-rate \
    -p 5032:80 \
    -e PostgresConnection="User ID=postgres;Password=postgres;Server=localhost;Port=5432;Database=currency-exchange;" \
    registry.dev.crosstech.com.tr/exchange-rate 