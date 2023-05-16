package main

import (
	"bytes"
	"encoding/json"
	"fmt"
	"log"
	"net/http"
	"sync"
	"time"
)

type Product struct {
	Name        string  `json:"name"`
	Description string  `json:"description"`
	Price       float64 `json:"price"`
	OwnerId     int     `json:"ownerId"`
}

var concurrentRequests = 100
var errCount = 0

var duration = float64(0)

func main() {
	wg := sync.WaitGroup{}
	wg.Add(concurrentRequests)

	body := Product{
		Name:        "product1",
		Description: "product1 description",
		Price:       111.111,
		OwnerId:     1, // Owner (user) should be created before.
	}
	bodyBytes, err := json.Marshal(&body)
	if err != nil {
		log.Fatal(err)
	}

	for i := 0; i < concurrentRequests; i++ {
		go func(count int) {
			now := time.Now()
			reader := bytes.NewReader(bodyBytes)
			resp, err := http.Post("http://localhost/products", "application/json", reader)
			if err != nil {
				errCount++
				fmt.Printf("request %d failed to load response: %v \n", count, err)
			} else if resp.StatusCode >= 400 {
				errCount++
				fmt.Printf("request %d failed to load response: %v (%d) \n", count, resp.Status, time.Since(now))
			} else {
				// fmt.Printf("request %d took %v to load\n", count, time.Since(now))
			}
			duration += time.Since(now).Seconds()
			wg.Done()
		}(i)
	}

	wg.Wait()
	fmt.Printf("average duration: %.2f seconds\n", duration/float64(concurrentRequests))
	fmt.Printf("%d from %d requests failed\n", errCount, concurrentRequests)
}
