FROM node:20-alpine AS build

WORKDIR /app

COPY package*.json ./
RUN npm ci

COPY . .
RUN npm run build

FROM nginx:alpine

RUN apk add --no-cache gettext

RUN rm -rf /usr/share/nginx/html/*

COPY --from=build /app/dist/debt-query-ui/browser /usr/share/nginx/html

COPY public/assets/config.template.json /usr/share/nginx/html/assets/config.template.json
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD sh -c "envsubst < /usr/share/nginx/html/assets/config.template.json > /usr/share/nginx/html/assets/config.json && nginx -g 'daemon off;'"