FROM node:alpine as build

WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . .

EXPOSE 5123

CMD [ "npm", "run", "dev" ]
