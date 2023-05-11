package main

import (
	"fmt"
	"net/http"
	"sync"
	"time"
)

var concurrentRequests = 100
var errCount = 0

var duration = float64(0)

func main() {
	wg := sync.WaitGroup{}
	wg.Add(concurrentRequests)

	for i := 0; i < concurrentRequests; i++ {
		go func(count int) {
			now := time.Now()
			resp, err := http.Get("http://localhost/users")
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