docker run -d \
    --name exchange-rate \
    -p 5032:80 \
    -e PostgresConnection="User ID=postgres;Password=EeDReSw8cX5@Ceg&+JTst3FFgeCfPRBJNYF@X!N4zq2vB4F*;Server=141.98.1.177;Port=5432;Database=currency-exchange;" \
    registry.dev.crosstech.com.tr/exchange-rate 